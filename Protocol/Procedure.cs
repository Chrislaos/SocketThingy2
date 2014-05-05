using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Protocol
{
    public class Procedure : DomainObject
    {
        public ObservableCollection<Execution> _executionlist = new ObservableCollection<Execution>();
        public ObservableCollection<Execution> Executionlist
        {
            get
            {
                return _executionlist;
            }
            set
            {
                _executionlist = value;
                OnPropertyChanged("ExecutionList");
            }
        }

        public String _name = null;
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public String _description = null;
        public String Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public DateTime _date = DateTime.Today;
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }

        }
    }
}
