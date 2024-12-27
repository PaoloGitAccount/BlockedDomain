using Xunit;

namespace Tests.UnitTests
{
    public class BlockedDomainServiceEdgeCasesTests
    {
        [Fact]
        public void TestSubdomainBlocking()
        {
            string[] A = new string[] { "subdomain.example.com", "example.com" };
            string[] B = new string[] { "example.com" };
            int[] expected = new int[] { };
            
            var result = Solution.solution(A, B);
            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestEmptyHosts()
        {
            string[] A = new string[] { };
            string[] B = new string[] { "example.com" };
            int[] expected = new int[] { };
            
            var result = Solution.solution(A, B);
            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestEmptyBlockedDomains()
        {
            string[] A = new string[] { "example.com", "test.com" };
            string[] B = new string[] { };
            int[] expected = new int[] { 0, 1 };
            
            var result = Solution.solution(A, B);
            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestSpecialCharactersInDomains()
        {
            string[] A = new string[] { "my-domain.example.com", "example.com" };
            string[] B = new string[] { "example.com" };
            int[] expected = new int[] { };
            
            var result = Solution.solution(A, B);
            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestAllBlocked()
        {
            string[] A = new string[] { "a.com", "b.com", "c.com" };
            string[] B = new string[] { "com" };
            int[] expected = new int[] { };
            
            var result = Solution.solution(A, B);
            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestNoneBlocked()
        {
            string[] A = new string[] { "a.com", "b.com", "c.com" };
            string[] B = new string[] { "xyz" };
            int[] expected = new int[] { 0, 1, 2 };
            
            var result = Solution.solution(A, B);
            
            Assert.Equal(expected, result);
        }
    }
}
