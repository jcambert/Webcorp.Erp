using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Webcorp.rx_mvvm
{
    public class DialogService : IDialogService
    {
        public void ShowInfo(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public MessageBoxResult ShowWarning(string title, string message)
        {
            return MessageBox.Show(message, title, MessageBoxButton.OKCancel, MessageBoxImage.Warning);
        }

        public void ShowError(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

       /* public bool ShowViewModel<T>(string title, ViewModelBase viewModel)where T :Window
        {
            var win = new DialogWindow(viewModel) { Title = title };
            win.ShowDialog();

            return win.CloseResult;
        }*/

        
    }
}
