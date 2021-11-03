using System;
using System.Collections;
using System.Collections.Generic;

namespace Artefact.GenderSystem
{
    [Serializable]
    class GenderPronouns : IEnumerable<GenderPronounData>
    {
        public GenderPronouns(string nominative, string objective, string possessiveDeterminer, string possessivePronoun, string reflexive)
        {
            this.nominative = nominative;
            this.objective = objective;
            this.possessiveDeterminer = possessiveDeterminer;
            this.possessivePronoun = possessivePronoun;
            this.reflexive = reflexive;
        }

        public readonly string nominative;
        public readonly string objective;
        public readonly string possessiveDeterminer;
        public readonly string possessivePronoun;
        public readonly string reflexive;

        public IEnumerator<GenderPronounData> GetEnumerator()
        {
            yield return new GenderPronounData(PronounType.Nominative, nominative);
            yield return new GenderPronounData(PronounType.Nominative, objective);
            yield return new GenderPronounData(PronounType.Possessive_Determiner, possessiveDeterminer);
            yield return new GenderPronounData(PronounType.Possessive_Pronoun, possessivePronoun);
            yield return new GenderPronounData(PronounType.Reflexive, reflexive);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    struct GenderPronounData
    {
        public GenderPronounData(PronounType pronounType, string text)
        {
            PronounType = pronounType;
            Text = text;
        }

        public PronounType PronounType { get; }
        public string Text { get; }
    }
}
