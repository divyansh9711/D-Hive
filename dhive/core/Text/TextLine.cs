namespace dhive.core.Text
{
    public sealed class TextLine{

        public TextLine (SourceText text, int start, int lenth, int  lengthIncludingLineBreak){
            Text = text;
            Start = start;
            Lenth = lenth;
            LengthIncludingLineBreak = lengthIncludingLineBreak;
        }

        public SourceText Text { get; }
        public int Start { get; }
        public int Lenth { get; }
        public int LengthIncludingLineBreak { get; }
        public int End => Start + Lenth;
        public TextSpan Span => new TextSpan(Start, Lenth);
        public TextSpan SpanIncludingLineBreak => new TextSpan(Start, LengthIncludingLineBreak);
        public override string ToString() => Text.ToString(Span);
    }
}