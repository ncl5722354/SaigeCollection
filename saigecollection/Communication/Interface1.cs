using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Communication
{
    [ComVisible(true)]
    [Guid("793E56DD-05C4-479F-80F0-0D7E5421A2D4")]
    public interface ITcpServer
    {
       void Start_Server();
    }
}
