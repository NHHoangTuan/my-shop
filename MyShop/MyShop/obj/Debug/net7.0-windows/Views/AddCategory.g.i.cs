﻿#pragma checksum "..\..\..\..\Views\AddCategory.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2427169E32E715751DDD1EE9D70361533AB05391"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using MyShop.Converter;
using MyShop.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MyShop.Views {
    
    
    /// <summary>
    /// AddCategory
    /// </summary>
    public partial class AddCategory : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 42 "..\..\..\..\Views\AddCategory.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameTermTextBox;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\Views\AddCategory.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DescriptionTermTextBox;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\..\Views\AddCategory.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddLogoImageButton;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\..\Views\AddCategory.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image AddedImage;
        
        #line default
        #line hidden
        
        
        #line 156 "..\..\..\..\Views\AddCategory.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveCat;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MyShop;V1.0.0.0;component/views/addcategory.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\AddCategory.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.NameTermTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.DescriptionTermTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.AddLogoImageButton = ((System.Windows.Controls.Button)(target));
            
            #line 128 "..\..\..\..\Views\AddCategory.xaml"
            this.AddLogoImageButton.Click += new System.Windows.RoutedEventHandler(this.AddLogoImageButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.AddedImage = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.SaveCat = ((System.Windows.Controls.Button)(target));
            
            #line 164 "..\..\..\..\Views\AddCategory.xaml"
            this.SaveCat.Click += new System.Windows.RoutedEventHandler(this.SaveCat_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

