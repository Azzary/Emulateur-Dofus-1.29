using System;
using System.Collections.Generic;
using System.Text;
using LeafAuth.PacketGestion;


namespace LeafAuth.connection
{
    class Authentication: Frame
    {
        [PacketAttribute("Af")]
        public void CheckQueue(Network.listenClient prmClient, string prmPacket)
        {
            if (prmClient.account.AccountIsVerifStade == 0)
            {
                if (prmClient.queue.Count == 0 || prmClient.queue[0] != prmClient)
                {
                    prmClient.send($"Af{prmClient.queue.IndexOf(prmClient) + 1}");
                }
                else
                    prmClient.send("Af");
            }


        }

        public void CheckLogin(Network.listenClient prmClient)
        {
            if (prmClient != null) 
            {
                if (prmClient.database.tableaccount.VerifAccount(prmClient))
                {

                    prmClient.database.tableserver.getServer(prmClient);
                    prmClient.database.tableserver.getCharacOnServer(prmClient);
                    prmClient.queue.Remove(prmClient);
                    Console.WriteLine("New Client Connected");
                    prmClient.account.AccountIsVerifStade = 1;
                    prmClient.send("Ad" + prmClient.account.UserName);
                    prmClient.send("Ac0");
                    prmClient.send(prmClient.account.PacketAH);
                    prmClient.send("AlK0");
                    prmClient.send("AQ" + prmClient.account.Qsecret);
                }
                else
                {
                    prmClient.isCo = false;
                    prmClient.queue.Remove(prmClient);
                }

            }

        }

        [PacketAttribute("Ax")]
        public void send_charac(Network.listenClient prmClient, string prmPacket)
        {
            if (prmClient.account.AccountIsVerifStade == 0)
            {
                prmClient.isCo = false;
                return;
            }
            prmClient.account.AccountIsVerifStade = 2;
            prmClient.send(prmClient.account.PacketAx);
        }

    }
}
