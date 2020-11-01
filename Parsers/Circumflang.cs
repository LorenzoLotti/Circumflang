using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Circumflang
{
    /// <summary>
    /// A list of <see cref="CircumflangGroup"/>.
    /// </summary>
    internal sealed class CircumflangData: IEnumerable<CircumflangGroup>
    {
        private readonly IEnumerable<CircumflangGroup> groups;

        /// <summary>
        /// Returns the count of <see cref="CircumflangGroup"/> objects of this instance.
        /// </summary>
        public int GroupsCount => groups.Count();

        /// <summary>
        /// Returns the <see cref="CircumflangGroup"/> at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index of the <see cref="CircumflangGroup"/> object.</param>
        /// <returns>The <see cref="CircumflangGroup"/> at <paramref name="index"/>.</returns>
        public CircumflangGroup this[int index] => groups.ElementAt(index);

        /// <summary>
        /// Initialize a new instance from a Circumflang string.
        /// </summary>
        /// <param name="text">The Circumflang string.</param>
        public CircumflangData(string text)
        {
            groups = from g in text.Replace("\n", "").Replace("\r", "").Split(new[] { "^^^^" }, StringSplitOptions.None)
                     select new CircumflangGroup(g);
        }

        IEnumerator<CircumflangGroup> IEnumerable<CircumflangGroup>.GetEnumerator()
        {
            return groups.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return groups.GetEnumerator();
        }
    }

    /// <summary>
    /// A list of <see cref="string"/>/<see cref="CircumflangValue"/> pairs.
    /// </summary>
    internal sealed class CircumflangGroup: IEnumerable<(string Tag, CircumflangValue Value)>
    {
        private readonly IDictionary<string, CircumflangValue> pairs;

        /// <summary>
        /// Returns the count of the <see cref="string"/>/<see cref="CircumflangValue"/> pairs of this instance.
        /// </summary>
        public int PairsCount => pairs.Count;

        /// <summary>
        /// Returns the tag list for this group.
        /// </summary>
        public IReadOnlyList<string> Tags => pairs.Select(pair => pair.Key).ToList();

        /// <summary>
        /// Returns the value list for this group.
        /// </summary>
        public IReadOnlyList<CircumflangValue> Values => pairs.Select(pair => pair.Value).ToList();

        /// <summary>
        /// Returns the <see cref="CircumflangValue"/> object associated with <paramref name="tag"/>.
        /// </summary>
        /// <param name="tag">A Circumflang tag.</param>
        /// <returns>The <see cref="CircumflangValue"/> object associated with <paramref name="tag"/>.</returns>
        public CircumflangValue this[string tag] => pairs[tag.ToLower()];

        /// <summary>
        /// Initialize a new instance from a Circumflang string of a group.
        /// </summary>
        /// <param name="text">The Circumflang string of a group.</param>
        public CircumflangGroup(string text)
        {
            var stringPairs = text.Split(new[] { "^^^" }, StringSplitOptions.None);
            pairs = new Dictionary<string, CircumflangValue>();
            foreach (var stringPair in stringPairs)
            {
                var pair = stringPair.Split(new[] { "^^" }, StringSplitOptions.None);
                try
                {
                    pairs.Add(pair.ElementAt(0).Trim().ToLower(), new CircumflangValue(pair.ElementAt(1).Trim()));
                }
                catch { }
            }
        }

        /// <summary>
        /// If the instance contains the input tag return true, otherwise, false.
        /// </summary>
        /// <param name="tag">Input tag.</param>
        /// <returns>A <see cref="bool"/> binary value.</returns>
        public bool ContainsTag(string tag)
        {
            return pairs.ContainsKey(tag.ToLower());
        }

        /// <summary>
        /// If the instance contains the input value return true, otherwise, false.
        /// </summary>
        /// <param name="value">Input value.</param>
        /// <returns>A <see cref="bool"/> binary value.</returns>
        public bool ContainsValue(CircumflangValue value)
        {
            return pairs.Values.Contains(value);
        }

        IEnumerator<(string Tag, CircumflangValue Value)> IEnumerable<(string Tag, CircumflangValue Value)>.GetEnumerator()
        {
            return pairs.Select(pair => (pair.Key, pair.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return pairs.Select(pair => (pair.Key, pair.Value)).GetEnumerator();
        }
    }

    /// <summary>
    /// A list of <see cref="string"/>.
    /// </summary>
    internal sealed class CircumflangValue: IEnumerable<string>
    {
        private readonly IEnumerable<string> values;

        /// <summary>
        /// The count of the strings.
        /// </summary>
        public int Count => values.Count();

        /// <summary>
        /// Returns the <see cref="string"/> object at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index of the <see cref="string"/> object.</param>
        /// <returns>The <see cref="string"/> object at <paramref name="index"/>.</returns>
        public string this[int index] => values.ElementAt(index);

        /// <summary>
        /// Initialize a new instance from a Circumflang string of a value.
        /// </summary>
        /// <param name="text">The Circumflang string of a value.</param>
        public CircumflangValue(string text)
        {
            values = from v in text.Split('^')
                     select v.Trim();
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator();
        }
    }
}
