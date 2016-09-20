using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Hauynite.ViewModels
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public event EventHandler IsBusyChanged;

		public event EventHandler IsValidChanged;

		readonly List<string> errors = new List<string>();

		bool isBusy = false;

		public ViewModelBase()
		{
			Validate();
		}

		public bool IsValid
		{
			get { return errors.Count == 0; }
		}

		protected List<string> Errors
		{
			get { return errors; }
		}

		public virtual string Error
		{
			get { return errors.Aggregate(new StringBuilder(), (b, s) => b.AppendLine(s)).ToString().Trim(); }
		}

		protected virtual void Validate()
		{
			OnPropertyChanged("IsValid");
			OnPropertyChanged("Errors");

			var method = IsValidChanged;
			if (method != null)
				method(this, EventArgs.Empty);
		}

		public virtual void ValidateProperty(Func<bool> validate, string error)
		{
			if (validate())
			{
				if (!Errors.Contains(error))
				{
					Errors.Add(error);
				}
			}
			else {
				Errors.Remove(error);
			}
		}

		public bool IsBusy
		{
			get { return isBusy; }
			set
			{
				if (isBusy != value)
				{
					isBusy = value;

					OnPropertyChanged("IsBusy");
					OnIsBusyChanged();
				}
			}
		}

		protected virtual void OnIsBusyChanged()
		{
			var method = IsBusyChanged;
			if (method != null)
				method(this, EventArgs.Empty);
		}

		protected virtual void OnPropertyChanged(string name)
		{
			var method = PropertyChanged;
			if (method != null)
				method(this, new PropertyChangedEventArgs(name));
		}
	}
}
