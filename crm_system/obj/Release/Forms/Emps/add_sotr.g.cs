﻿#pragma checksum "..\..\..\..\Forms\Emps\add_sotr.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ED9725186F3849FC4CE1616961D70BA610DFF42D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using crm_system;


namespace crm_system {
    
    
    /// <summary>
    /// add_sotr
    /// </summary>
    public partial class add_sotr : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\..\Forms\Emps\add_sotr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox orgs;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\Forms\Emps\add_sotr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox name;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Forms\Emps\add_sotr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox surname;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\Forms\Emps\add_sotr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox lastname;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\Forms\Emps\add_sotr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox job_title;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Forms\Emps\add_sotr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button add_or_upd;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\Forms\Emps\add_sotr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/crm_system;component/forms/emps/add_sotr.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Forms\Emps\add_sotr.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\..\Forms\Emps\add_sotr.xaml"
            ((crm_system.add_sotr)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.orgs = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.name = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\..\..\Forms\Emps\add_sotr.xaml"
            this.name.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.name_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.surname = ((System.Windows.Controls.TextBox)(target));
            
            #line 23 "..\..\..\..\Forms\Emps\add_sotr.xaml"
            this.surname.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.surname_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lastname = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\..\..\Forms\Emps\add_sotr.xaml"
            this.lastname.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.lastname_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.job_title = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.add_or_upd = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\..\Forms\Emps\add_sotr.xaml"
            this.add_or_upd.Click += new System.Windows.RoutedEventHandler(this.add_or_upd_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cancel = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\..\Forms\Emps\add_sotr.xaml"
            this.cancel.Click += new System.Windows.RoutedEventHandler(this.cancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

