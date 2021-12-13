using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.TextBuilders
{
    internal abstract class BaseTextBuilder<T> where T : BaseTextBuildData<T>
    {
        public string Text { get { return strText; } set { strText = value; BuildString(); } }

        private string strText;

        public List<T> BuildData { get; protected set; }

        public BaseTextBuilder(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Turn a string into a <see cref="List{T}"/> of <typeparamref name="T"/>
        /// </summary>
        protected abstract void BuildString();

        public List<List<T>> Split(string separator)
        {
            List<List<T>> toReturn = new List<List<T>>();
            List<T> currentLine = new List<T>();

            foreach (T buildData in BuildData)
            {
                string[] texts = buildData.Text.Split(separator);
                for (int i = 0; i < texts.Length; i++)
                {
                    if (i > 0)
                    {
                        toReturn.Add(currentLine);
                        currentLine = new List<T>();
                    }
                    T temp = buildData.Clone();
                    temp.Text = texts[i];
                    currentLine.Add(temp);
                }
            }
            toReturn.Add(currentLine);

            return toReturn;
        }
    }

    internal abstract class BaseTextBuildData<T>
    {
        public string Text { get; set; }

        public BaseTextBuildData(string text)
        {
            Text = text;
        }

        public abstract T Clone();
    }
}
