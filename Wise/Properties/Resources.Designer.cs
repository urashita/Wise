﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.18063
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wise.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Wise.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   厳密に型指定されたこのリソース クラスを使用して、すべての検索リソースに対し、
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Delete selected item に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string MessageBoxDeleteItem {
            get {
                return ResourceManager.GetString("MessageBoxDeleteItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   There is an empty field に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string MessageBoxEmpty {
            get {
                return ResourceManager.GetString("MessageBoxEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cannot add because the same item already exists に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string MessageBoxSameItem {
            get {
                return ResourceManager.GetString("MessageBoxSameItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Same search word already exists に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string MessageBoxSameSearchWordExist {
            get {
                return ResourceManager.GetString("MessageBoxSameSearchWordExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Same URL already exists に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string MessageBoxSameURLExist {
            get {
                return ResourceManager.GetString("MessageBoxSameURLExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Site Name already exists に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string MessageBoxSiteNameExist {
            get {
                return ResourceManager.GetString("MessageBoxSiteNameExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   URL is wrong に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string MessageBoxURL {
            get {
                return ResourceManager.GetString("MessageBoxURL", resourceCulture);
            }
        }
    }
}
