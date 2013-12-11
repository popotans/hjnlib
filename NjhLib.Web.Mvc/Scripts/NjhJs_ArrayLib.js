//array是否包含
Array.prototype.contains = function (obj) {
    var i = this.length;
    while (i--) {
        if (this[i] == obj) {
            return true;
        }
    }
    return false;
}
//移除元素
Array.prototype.remove = function (dx) {
    if (isNaN(dx) || dx > this.length) {
        return false;
    }
    for (var i = 0, n = 0; i < this.length; i++) {
        if (this[i] != this[dx]) {
            this[n++] = this[i]
        }
    }
    this.length -= 1
}
// 去除数组中的重复元素
Array.prototype.unique = function (obj) {
    var newArr = new Array();
    for (i = 0; i < this.length; i++) {
        if (!newArr.contains(this[i])) {
            newArr.push(this[i]);
        }
    }
    return newArr;
}
Array.prototype.unique2 = function () {
    var n = {}, r = []; //n为hash表，r为临时数组
    for (var i = 0; i < this.length; i++) //遍历当前数组
    {
        if (!n[this[i]]) //如果hash表中没有当前项
        {
            n[this[i]] = true; //存入hash表
            r.push(this[i]); //把当前数组的当前项push到临时数组里面
        }
    }
    return r;
}
//数组随机排序	
Array.prototype.aSort = function (method) {
    function Sort(a, b) {
        if (method == 0 || method == 1) {
            if (a > b) { if (method == 0) { return 1 } else { return -1 } }
            if (a < b) { if (method == 0) { return -1 } else { return 1 } }
            else { return 0 }
        }
        else if (method == 2) { return Math.random() > .5 ? -1 : 1; } //用Math.random()函数生成0~1之间的随机数与0.5比较，返回-1或1       	
    }
    this.sort(Sort);
};
//copy数组
Array.prototype.copy = function () { return this.slice(); };

Array.prototype.indexofobj = function (str) {
    for (var q = 0; q < this.length; q++) {
        if (this[q].gName == str) { return q; }
    }
    return -1;
};
//返回数组中指定字符串的索引
Array.prototype.indexof = function (str) {
    for (var q = 0; q < this.length; q++) {
        if (this[q] == str) { return q; }
    }
    return -1;
};

//在数组中删除指定项	
Array.prototype.delItem = function (o) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == o) { this.splice(i, 1); break; }
    }

};
//在数组任意索引处删除一项	
Array.prototype.delIndex = function (index) { this.splice(index, 1) };
//在数组任意索引处删除多项	
Array.prototype.del = function () {
    var opts = this.sort.call(arguments, Function('a,b', 'return a > b?-1:1;'));
    for (var i = 0; i < opts.length; i++) { this.splice(opts[i], 1); }
    return this;
};
//在数组任意索引后增加一项或多项	
Array.prototype.addIndex = function (index, arr) { this.splice(index + 1, 0, arr) };
//返回数组中最大项
Array.prototype.max = function () {
    return Math.max.apply({}, this);
};

//返回数组中最小项
Array.prototype.min = function () {
    return Math.min.apply({}, this);
};
