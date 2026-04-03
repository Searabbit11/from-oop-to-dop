using System;
using System.Collections.Generic;

namespace Study.OOP._02._Behavioral
{
    public class ObservableValue<T>
    {
        private T value;
        public event Action<T> OnValueChanged;

        public T Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (EqualityComparer<T>.Default.Equals(this.value, value)
                    == false)
                {
                    this.value = value;
                    OnValueChanged?.Invoke(this.value);
                }
            }
        }

        public ObservableValue()
        {
            this.value = default(T);
        }
        
        public ObservableValue(T value)
        {
            this.value = value;
        }
        
        // 내부값을 암시적으로 변환
        public static implicit operator T(ObservableValue<T> observable)
        {
            return observable.Value;
        }
    }
}