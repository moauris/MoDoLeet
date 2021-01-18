using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SolutionValidator
{
    public class Parm
    {
        public Parm(object val)
        {
            Value = val;
        }
        public Type ParmType{ get => Value.GetType(); }
        public object Value { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Parm parm &&
                   EqualityComparer<Type>.Default.Equals(ParmType, parm.ParmType) &&
                   EqualityComparer<object>.Default.Equals(Value, parm.Value);
        }

        public override int GetHashCode()
        {
            int hashCode = -1536674135;
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(ParmType);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Value);
            return hashCode;
        }

        public static bool operator ==(Parm left, Parm right) => left.Equals(right);
        public static bool operator !=(Parm left, Parm right) => !left.Equals(right);

    }

    public class Parms : ICollection<Parm>
    {
        public Parm this[int index] => _contents[index];
        private Parm[] _contents = new Parm[0];
        private int _count = 0;
        public int Count => _count;

        public bool IsReadOnly => false;

        public void Add(Parm item)
        {
            Parm[] temp = new Parm[_count + 1];
            _contents.CopyTo(temp, 0);
            temp[_count] = item;
            _contents = temp;
            _count++;
        }

        public void Clear()
        {
            _contents = new Parm[0];
            _count = 0;
        }

        public bool Contains(Parm item)
        {
            foreach (Parm parm in _contents)
            {
                if (parm == item)
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(Parm[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Parm> GetEnumerator()
        {
            foreach (Parm parm in _contents)
                yield return parm;
        }

        public bool Remove(Parm item)
        {
            if (!Contains(item))
                return false;

            Parm[] newContents = new Parm[_count - 1];
            int index = 0;
            bool found = false;
            while (index < _count - 1)
            {
                if (!found)
                    newContents[index] = _contents[index];
                else
                    newContents[index] = _contents[index + 1];

                if (_contents[index] == item) found = true;
            }
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public object[] GetAllParmValues()
        {
            object[] vals = new object[_count];
            for (int i = 0; i < _count; i++)
            {
                vals[i] = _contents[i].Value;
            }
            return vals;
        }
    }
}
