using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Protocol
{
    public class Execution : DomainObject
    {

        public enum ExecutionStates { LOADED, STARTED, RESUMED, PAUSED, FINISHED, TERMINATED, CAKE = 1337 }

        private Step _currentStep = null;

        public Step CurrentStep
        {
            get
            {
                return _currentStep;
            }
            set
            {
                _currentStep = value;
                OnPropertyChanged("CurrentStep");
            }

        }

        private Sequence _currentSequence = new Sequence();
        public Sequence CurrentSequence
        {
            get
            {
                return _currentSequence;
            }
            set
            {
                _currentSequence = value;
                OnPropertyChanged("CurrentSequence");
            }

        }

        private ExecutionStates _state = ExecutionStates.CAKE;
        public ExecutionStates State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                OnPropertyChanged("State");
            }
        }
    }
}
