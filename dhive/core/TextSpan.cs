using System;

namespace dhive.core
{
    public struct TextSpan{
        public TextSpan(int start, int length){
            Start = start;
            Length = length;
            End = start + length;
        }

        public int Start { get; }
        public int End {get; }
        public int Length { get; }

        internal static TextSpan FromBounds(int start, int end){
            var length = end - start;
            return new TextSpan(start, length);
        }
    }
}