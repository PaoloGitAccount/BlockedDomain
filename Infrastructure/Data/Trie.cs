using System;

namespace Infrastructure.Data
{
    public class Trie
    {
        private readonly TrieNode _root;

        public Trie()
        {
            _root = new TrieNode();
        }

        public void Insert(string domain)
        {
            var currentNode = _root;
            var reversedDomain = ReverseDomain(domain);
            foreach (var ch in reversedDomain)
            {
                if (!currentNode.Children.ContainsKey(ch))
                {
                    currentNode.Children[ch] = new TrieNode();
                }
                currentNode = currentNode.Children[ch];
            }
            currentNode.IsEndOfDomain = true;
        }

        public bool IsBlocked(string domain)
        {
            var currentNode = _root;
            var reversedDomain = ReverseDomain(domain);
            foreach (var ch in reversedDomain)
            {
                if (currentNode.IsEndOfDomain)
                {
                    return true;
                }
                if (!currentNode.Children.ContainsKey(ch))
                {
                    return false;
                }
                currentNode = currentNode.Children[ch];
            }
            return currentNode.IsEndOfDomain;
        }

        private string ReverseDomain(string domain)
        {
            var parts = domain.Split('.');
            Array.Reverse(parts);
            return string.Join('.', parts);
        }
    }
}
