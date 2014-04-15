using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Demo.Protocol
{
    public class Sequence : DomainObject
    {
        //observable collection?
        private ObservableCollection<Step> _steplist = new ObservableCollection<Step>();
        public ObservableCollection<Step> StepList
        {
            get
            {
                return _steplist;
            }
            set
            {
                _steplist = value;
                OnPropertyChanged("StepList");
            }
        }

        private String _sequenceFileName = null;
        public String SequenceFileName
        {
            get
            {
                return _sequenceFileName;
            }
            set
            {
                _sequenceFileName = value;
                OnPropertyChanged("SequenceFileName");
            }
        }
    }
}
