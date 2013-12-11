using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace NjhLib.Utils
{
    [Serializable]
    internal class IPArrayList
    {
        private bool isSorted = false;
        private ArrayList ipNumList = new ArrayList();
        private uint ipmask;

        public IPArrayList(uint mask)
        {
            ipmask = mask;
        }

        public void Add(uint IPNum)
        {
            isSorted = false;
            ipNumList.Add(IPNum & ipmask);
        }

        public bool Check(uint IPNum)
        {
            bool found = false;
            if (ipNumList.Count > 0)
            {
                if (!isSorted)
                {
                    ipNumList.Sort();
                    isSorted = true;
                }
                IPNum = IPNum & ipmask;
                if (ipNumList.BinarySearch(IPNum) >= 0) found = true;
            }
            return found;
        }

        public void Clear()
        {
            ipNumList.Clear();
            isSorted = false;
        }

        public override string ToString()
        {
            StringBuilder buf = new StringBuilder();
            foreach (uint ipnum in ipNumList)
            {
                if (buf.Length > 0) buf.Append("\r\n");
                buf.Append(((int)ipnum & 0xFF000000) >> 24).Append('.');
                buf.Append(((int)ipnum & 0x00FF0000) >> 16).Append('.');
                buf.Append(((int)ipnum & 0x0000FF00) >> 8).Append('.');
                buf.Append(((int)ipnum & 0x000000FF));
            }
            return buf.ToString();
        }

        public uint Mask
        {
            get
            {
                return ipmask;
            }
        }
    }
    [Serializable]
    public class IPList
    {
        private ArrayList ipRangeList = new ArrayList();
        private SortedList maskList = new SortedList();
        public ArrayList usedList = new ArrayList();

        public IPList()
        {
            uint mask = 0x00000000;
            for (int level = 1; level < 33; level++)
            {
                mask = (mask >> 1) | 0x80000000;
                maskList.Add(mask, level);
                ipRangeList.Add(new IPArrayList(mask));
            }
        }

        private uint parseIP(string IPNumber)
        {
            uint res = 0;
            string[] elements = IPNumber.Split(new Char[] { '.' });
            if (elements.Length == 4)
            {
                res = (uint)Convert.ToInt32(elements[0]) << 24;
                res += (uint)Convert.ToInt32(elements[1]) << 16;
                res += (uint)Convert.ToInt32(elements[2]) << 8;
                res += (uint)Convert.ToInt32(elements[3]);
            }
            return res;
        }

        public void Add(string ipNumber)
        {
            this.Add(parseIP(ipNumber));
        }

        public void Add(uint ip)
        {
            ((IPArrayList)ipRangeList[31]).Add(ip);
            if (!usedList.Contains((int)31))
            {
                usedList.Add((int)31);
                usedList.Sort();
            }
        }

        public void Add(string ipNumber, string mask)
        {
            this.Add(parseIP(ipNumber), parseIP(mask));
        }

        public void Add(uint ip, uint umask)
        {
            object Level = maskList[umask];
            if (Level != null)
            {
                ip = ip & umask;
                ((IPArrayList)ipRangeList[(int)Level - 1]).Add(ip);
                if (!usedList.Contains((int)Level - 1))
                {
                    usedList.Add((int)Level - 1);
                    usedList.Sort();
                }
            }
        }

        public void Add(string ipNumber, int maskLevel)
        {
            this.Add(parseIP(ipNumber), (uint)maskList.GetKey(maskList.IndexOfValue(maskLevel)));
        }

        public void AddRange(string fromIP, string toIP)
        {
            this.AddRange(parseIP(fromIP), parseIP(toIP));
        }

        public void AddRange(uint fromIP, uint toIP)
        {
            // If the order is not asending, switch the IP numbers.
            if (fromIP > toIP)
            {
                uint tempIP = fromIP;
                fromIP = toIP;
                toIP = tempIP;
            }
            if (fromIP == toIP)
            {
                this.Add(fromIP);
            }
            else
            {
                uint diff = toIP - fromIP;
                int diffLevel = 1;
                uint range = 0x80000000;
                if (diff < 256)
                {
                    diffLevel = 24;
                    range = 0x00000100;
                }
                while (range > diff)
                {
                    range = range >> 1;
                    diffLevel++;
                }
                uint mask = (uint)maskList.GetKey(maskList.IndexOfValue(diffLevel));
                uint minIP = fromIP & mask;
                if (minIP < fromIP) minIP += range;
                if (minIP > fromIP)
                {
                    this.AddRange(fromIP, minIP - 1);
                    fromIP = minIP;
                }
                if (fromIP == toIP)
                {
                    this.Add(fromIP);
                }
                else
                {
                    if ((minIP + (range - 1)) <= toIP)
                    {
                        this.Add(minIP, mask);
                        fromIP = minIP + range;
                    }
                    if (fromIP == toIP)
                    {
                        this.Add(toIP);
                    }
                    else
                    {
                        if (fromIP < toIP) this.AddRange(fromIP, toIP);
                    }
                }
            }
        }

        public bool CheckNumber(string ipNumber)
        {
            return this.CheckNumber(parseIP(ipNumber)); ;
        }

        public bool CheckNumber(uint ip)
        {
            bool found = false;
            int i = 0;
            while (!found && i < usedList.Count)
            {
                found = ((IPArrayList)ipRangeList[(int)usedList[i]]).Check(ip);
                i++;
            }
            return found;
        }

        public void Clear()
        {
            foreach (int i in usedList)
            {
                ((IPArrayList)ipRangeList[i]).Clear();
            }
            usedList.Clear();
        }

        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();
            foreach (int i in usedList)
            {
                buffer.Append("\r\nRange with mask of ").Append(i + 1).Append("\r\n");
                buffer.Append(((IPArrayList)ipRangeList[i]).ToString());
            }
            return buffer.ToString();
        }


    }
}
