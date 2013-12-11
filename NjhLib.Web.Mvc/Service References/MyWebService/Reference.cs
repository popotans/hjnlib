﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NjhLib.Web.Mvc.MyWebService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MyWebService.basicSoap")]
    public interface basicSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string HelloWorld(string u, string p);
        
        // CODEGEN: Generating message contract since message HelloWorld2Request has headers
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld2", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        NjhLib.Web.Mvc.MyWebService.HelloWorld2Response HelloWorld2(NjhLib.Web.Mvc.MyWebService.HelloWorld2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetStudent", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        NjhLib.Web.Mvc.MyWebService.Student GetStudent();
        
        // CODEGEN: Parameter 'filebyte' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UploadFile", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        NjhLib.Web.Mvc.MyWebService.UploadFileResponse UploadFile(NjhLib.Web.Mvc.MyWebService.UploadFileRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class MyHeader : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string userIdField;
        
        private string passwordField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string UserId {
            get {
                return this.userIdField;
            }
            set {
                this.userIdField = value;
                this.RaisePropertyChanged("UserId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
                this.RaisePropertyChanged("Password");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
                this.RaisePropertyChanged("AnyAttr");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Student : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string nameField;
        
        private string pwdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
                this.RaisePropertyChanged("Name");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Pwd {
            get {
                return this.pwdField;
            }
            set {
                this.pwdField = value;
                this.RaisePropertyChanged("Pwd");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="HelloWorld2", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class HelloWorld2Request {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public NjhLib.Web.Mvc.MyWebService.MyHeader MyHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string content;
        
        public HelloWorld2Request() {
        }
        
        public HelloWorld2Request(NjhLib.Web.Mvc.MyWebService.MyHeader MyHeader, string content) {
            this.MyHeader = MyHeader;
            this.content = content;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="HelloWorld2Response", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class HelloWorld2Response {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string HelloWorld2Result;
        
        public HelloWorld2Response() {
        }
        
        public HelloWorld2Response(string HelloWorld2Result) {
            this.HelloWorld2Result = HelloWorld2Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UploadFile", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UploadFileRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] filebyte;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string fileextension;
        
        public UploadFileRequest() {
        }
        
        public UploadFileRequest(byte[] filebyte, string fileextension) {
            this.filebyte = filebyte;
            this.fileextension = fileextension;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UploadFileResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UploadFileResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public int UploadFileResult;
        
        public UploadFileResponse() {
        }
        
        public UploadFileResponse(int UploadFileResult) {
            this.UploadFileResult = UploadFileResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface basicSoapChannel : NjhLib.Web.Mvc.MyWebService.basicSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class basicSoapClient : System.ServiceModel.ClientBase<NjhLib.Web.Mvc.MyWebService.basicSoap>, NjhLib.Web.Mvc.MyWebService.basicSoap {
        
        public basicSoapClient() {
        }
        
        public basicSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public basicSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public basicSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public basicSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string HelloWorld(string u, string p) {
            return base.Channel.HelloWorld(u, p);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        NjhLib.Web.Mvc.MyWebService.HelloWorld2Response NjhLib.Web.Mvc.MyWebService.basicSoap.HelloWorld2(NjhLib.Web.Mvc.MyWebService.HelloWorld2Request request) {
            return base.Channel.HelloWorld2(request);
        }
        
        public string HelloWorld2(NjhLib.Web.Mvc.MyWebService.MyHeader MyHeader, string content) {
            NjhLib.Web.Mvc.MyWebService.HelloWorld2Request inValue = new NjhLib.Web.Mvc.MyWebService.HelloWorld2Request();
            inValue.MyHeader = MyHeader;
            inValue.content = content;
            NjhLib.Web.Mvc.MyWebService.HelloWorld2Response retVal = ((NjhLib.Web.Mvc.MyWebService.basicSoap)(this)).HelloWorld2(inValue);
            return retVal.HelloWorld2Result;
        }
        
        public NjhLib.Web.Mvc.MyWebService.Student GetStudent() {
            return base.Channel.GetStudent();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        NjhLib.Web.Mvc.MyWebService.UploadFileResponse NjhLib.Web.Mvc.MyWebService.basicSoap.UploadFile(NjhLib.Web.Mvc.MyWebService.UploadFileRequest request) {
            return base.Channel.UploadFile(request);
        }
        
        public int UploadFile(byte[] filebyte, string fileextension) {
            NjhLib.Web.Mvc.MyWebService.UploadFileRequest inValue = new NjhLib.Web.Mvc.MyWebService.UploadFileRequest();
            inValue.filebyte = filebyte;
            inValue.fileextension = fileextension;
            NjhLib.Web.Mvc.MyWebService.UploadFileResponse retVal = ((NjhLib.Web.Mvc.MyWebService.basicSoap)(this)).UploadFile(inValue);
            return retVal.UploadFileResult;
        }
    }
}