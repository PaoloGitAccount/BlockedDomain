using Application.Interfaces;

namespace Infrastructure.Data
{
    public class TrieDomainBlockChecker : IDomainBlockChecker
    {
        private readonly Trie _trie;

        public TrieDomainBlockChecker(Trie trie)
        {
            _trie = trie;
        }

        public bool IsBlocked(string domain)
        {
            return _trie.IsBlocked(domain);
        }
    }
}
