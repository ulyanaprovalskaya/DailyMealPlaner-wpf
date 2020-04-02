using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.ComponentModel;

namespace DailyMealPlaner.Presentation_Layer
{
	public class TreeViewItemBase : INotifyPropertyChanged
	{
		private bool isSelected;
		public bool IsSelected
		{
			get { return this.isSelected; }
			set
			{
				if (value != this.isSelected)
				{
					this.isSelected = value;
					NotifyPropertyChanged("IsSelected");
				}
			}
		}

		private bool isExpanded;
		public bool IsExpanded
		{
			get { return this.isExpanded; }
			set
			{
				if (value != this.isExpanded)
				{
					this.isExpanded = value;
					NotifyPropertyChanged("IsExpanded");
				}
			}
		}

		private ImageSource imageSource;
		public ImageSource ImageSource
		{
			get { return this.imageSource; }
			set
			{
				if (value != this.imageSource)
				{
					this.imageSource = value;
					NotifyPropertyChanged("ImageSource");
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged(string propName)
		{
			if (this.PropertyChanged != null)
				this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
	}
}
