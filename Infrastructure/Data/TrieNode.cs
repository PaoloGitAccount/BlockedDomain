using System.Collections.Generic;

namespace Infrastructure.Data
{
    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; set; }
        public bool IsEndOfDomain { get; set; }

        public TrieNode()
        {
            Children = new Dictionary<char, TrieNode>();
            IsEndOfDomain = false;
        }
    }
}
