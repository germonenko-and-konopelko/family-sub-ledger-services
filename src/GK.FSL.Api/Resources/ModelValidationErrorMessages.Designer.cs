﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GK.FSL.Api.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ModelValidationErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ModelValidationErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GK.FSL.Api.Resources.ModelValidationErrorMessages", typeof(ModelValidationErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email address is malformed..
        /// </summary>
        public static string EmailAddressIsMalformed {
            get {
                return ResourceManager.GetString("EmailAddressIsMalformed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email address is required..
        /// </summary>
        public static string EmailAddressIsRequired {
            get {
                return ResourceManager.GetString("EmailAddressIsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email address has max length of 250 symbols..
        /// </summary>
        public static string EmailAddressMaxLength {
            get {
                return ResourceManager.GetString("EmailAddressMaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to First name is required..
        /// </summary>
        public static string FirstNameIsRequired {
            get {
                return ResourceManager.GetString("FirstNameIsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to First name has max length of 50 symbols..
        /// </summary>
        public static string FirstNameMaxLength {
            get {
                return ResourceManager.GetString("FirstNameMaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Last name is required..
        /// </summary>
        public static string LastNameIsRequired {
            get {
                return ResourceManager.GetString("LastNameIsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Last name max has length of 50 symbols..
        /// </summary>
        public static string LastNameMaxLength {
            get {
                return ResourceManager.GetString("LastNameMaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Login is required..
        /// </summary>
        public static string LoginIsRequired {
            get {
                return ResourceManager.GetString("LoginIsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password is required..
        /// </summary>
        public static string PasswordIsRequired {
            get {
                return ResourceManager.GetString("PasswordIsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password has max lengths of 200 symbols..
        /// </summary>
        public static string PasswordMaxLength {
            get {
                return ResourceManager.GetString("PasswordMaxLength", resourceCulture);
            }
        }
    }
}
