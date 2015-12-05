using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace Webcorp.lib.ui
{
  /*  public class WindowBase : Window, IInitializable,ILoggable
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        private ICommandObserver<Unit> _minimizeWindowCommand;
        private ICommandObserver<Unit> _maximizeWindowCommand;
        private ICommandObserver<Unit> _closeWindowCommand;
        private ICommandObserver<Unit> _aboutWindowCommand;
        private ICommandObserver<Unit> _preferencesWindowCommand;

        private IPropertySubject<bool> _canMinimize;
        private IPropertySubject<bool> _canMaximize;
        private IPropertySubject<bool> _canClose;
        private IPropertySubject<bool> _canAbout;
        private IPropertySubject<bool> _canPreferences;

        public WindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowBase), new FrameworkPropertyMetadata(typeof(WindowBase)));
        }

        [Inject]
        public IKernel Container { get; set; }

        [Inject]
        public ILogger Logger { get; set; }

        //Relay Commands
        public ICommand MinimizeWindowCommand => _minimizeWindowCommand;

        public ICommand MaximizeWindowCommand => _maximizeWindowCommand;

        public ICommand CloseWindowCommand => _closeWindowCommand;

        public ICommand AboutWindowCommand => _aboutWindowCommand;

        public ICommand PreferencesWindowCommand => _preferencesWindowCommand;


        public virtual bool CanMinimize
        {
            get { return _canMinimize?.Value ?? false; }
            set { _canMinimize.Value = value; }
        }

        public virtual bool CanMaximize
        {
            get { return _canMaximize?.Value ?? false; }
            set { _canMaximize.Value = value; }
        }

        public virtual bool CanClose
        {
            get { return _canClose?.Value ?? false; }
            set { _canClose.Value = value; }
        }

        public virtual bool CanAbout
        {
            get { return _canAbout?.Value ?? false; }
            set { _canAbout.Value = value; }
        }

        public virtual bool CanPreferences
        {
            get { return _canPreferences?.Value ?? false; }
            set { _canPreferences.Value = value; }
        }

        private void OnMinimizeWindow()
        {
            SystemCommands.MinimizeWindow(this);
        }
        private void OnMaximizeWindow()
        {
            if ((WindowState == WindowState.Normal))
            {
                SystemCommands.MaximizeWindow(this);
            }
        }
        private void OnCloseWindow()
        {
            SystemCommands.CloseWindow(this);
        }
        private void OnAbout()
        {
            MessageBox.Show("A propos .....");
        }
        private void OnPreferences()
        {
            MessageBox.Show("Preferences ....");
        }

        public void Initialize()
        {
            CreateProperties<WindowBase>();
        }

        protected virtual void CreateProperties<W>() where W : WindowBase
        {

            CreateProperty<W>(i => i.CanMinimize, CanMinimize, ref _canMinimize, ref _minimizeWindowCommand, OnMinimizeWindow);
            CreateProperty<W>(i => i.CanMaximize, CanMaximize, ref _canMaximize, ref _maximizeWindowCommand, OnMaximizeWindow);
            CreateProperty<W>(i => i.CanClose, CanClose , ref _canClose, ref _closeWindowCommand, OnCloseWindow);
            CreateProperty<W>(i => i.CanAbout, CanAbout, ref _canAbout, ref _aboutWindowCommand, OnAbout);
            CreateProperty<W>(i => i.CanPreferences, CanPreferences, ref _canPreferences, ref _preferencesWindowCommand, OnPreferences);

        }


        protected virtual void CreateProperty<W>(Expression<Func<W, bool>> property, bool canCmd, ref IPropertySubject<bool> _canSubject, ref ICommandObserver<Unit> _canCommand, Action action) where W : WindowBase
        {
            _canSubject = Get<W>().CreateProperty(property, canCmd);
            _canCommand = CreateCommand<W>(canCmd);
            ShouldDispose(_canSubject.Subscribe(_canCommand.SetCanExecute));
            ShouldDispose(_canSubject.Subscribe(_ =>
            {
                //Debugger.Break();
            }));
            ShouldDispose(_canCommand.OnExecute.Subscribe(_ => action()));
            //   _canSubject.Value = false;
        }
        protected ICommandObserver<Unit> CreateCommand<W>(bool IsEnabled = true) where W : WindowBase
        {
            return Get<W>().CreateCommand<Unit>(IsEnabled);
        }

        protected IPropertyProvider<W> Get<W>() where W : WindowBase => Container.Resolve<IPropertyProvider<W>>(this);

        //Dependency Properties
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(String), typeof(WindowBase));
        public String Header
        {
            get { return (String)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }


        public void ShouldDispose(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        #region Logger
        public void Debug(string message, [CallerMemberName] string caller = "")
        {
            Logger.Debug(message, caller);
        }
        public void Debug([CallerMemberName] string message = "")
        {
            Logger.Debug(message);
        }
        public void Info(string message, [CallerMemberName] string caller = "")
        {
            Logger.Info(message, caller);
        }
        public void Info([CallerMemberName] string message = "")
        {
            Logger.Info(message);
        }
        public void Warn(string message, [CallerMemberName] string caller = "")
        {
            Logger.Warn(message, caller);
        }
        public void Warn([CallerMemberName] string message = "")
        {
            Logger.Warn(message);
        }
        public void Exception(string message, [CallerMemberName] string caller = "")
        {
            Logger.Exception(message, caller);
        }
        public void Exception([CallerMemberName] string message = "")
        {
            Logger.Exception(message);
        }


        #endregion
    }*/
}
