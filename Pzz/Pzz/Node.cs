using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pzz
{
    public class Node
    {
        public int[] state = new int[10];
        public int[] state1 = new int[26];
        public int f = 0, g = 0, h = 0, location_empty = 0; //g là chi phí từ nút bắt đầu đến nút thứ n
        public Node parent = null;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
