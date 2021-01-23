using LeafWorld.Network;
using LeafWorld.PacketGestion;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Game.Character
{
    class GestionCharacter
    {

        [PacketAttribute("AA")]
        public void CreateCharacter(listenClient prmClient, string prmPacket)
        {
            string[] packet = prmPacket.Substring(2).Split("|");
            if (packet.Length == 6)
            {
                int gfxID = int.Parse(packet[1]) * 10;
                if (packet[2] == "1")
                {
                    gfxID++;
                }
                byte classe = byte.Parse(packet[1]);
                int[] StartPos = World.WorldConfig.GetPosStart(classe);
                if (StartPos[1] == 0)
                {
                    prmClient.send("BN");
                    prmClient.send(send_character(prmClient));
                    return;
                }
                account.character.Character character = new account.character.Character(prmClient.database.getidOfCreation(), packet[0], 1, 0, gfxID, StartPos[0], StartPos[1], int.Parse(packet[3]),
                    int.Parse(packet[4]), int.Parse(packet[5]), int.Parse(packet[2]), classe, 0, 1000, 0, 0, 999, 99, 50, 10000, 6, 3, 0, 0, 0, 0, 0, true);
                Game.Spell.SpellTemplate.AddSpell(character);                                             
                prmClient.account.ListCharacter.Add(character);
            }
            prmClient.send("BN");
            prmClient.send(send_character(prmClient));

        }


        [PacketAttribute("AD")]
        public void DelCharacter(Network.listenClient prmClient, string prmPacket)
        {
            foreach (account.character.Character charac in prmClient.account.ListCharacter)
            {
                string x = prmPacket.Substring(2).Split("|")[0];
                if (x == charac.id.ToString())
                {
                    prmClient.account.ListRemoveCharacter.Add(charac);
                    prmClient.account.ListCharacter.Remove(charac);
                    break;
                }
            }
            prmClient.send("BN");
            prmClient.send(send_character(prmClient));

        }

        [PacketAttribute("Ak0")]
        public void ListCharacter(Network.listenClient prmClient, string prmPacket)
        {
            prmClient.send("BN");
            prmClient.send("BN");
            prmClient.send("BN");
            prmClient.send("Aq1");
            prmClient.send(send_character(prmClient));
        }

            [PacketAttribute("GC1")]
        public void StatCharacter(Network.listenClient prmClient, string prmPacket)
        {
            prmClient.send("53;2|54;1|55;1|56;1|57;1|59;1|61;1|62;1|63;1|64;0|65;0|66;0|67;0|68;1|69;1|70;1|71;1|72;1|73;1|74;1|75;2|76;1|77;0|78;0|79;0|80;0|81;0|82;0|83;0|84;1|85;0|86;1|87;0|88;0|89;0|91;1|92;1|93;1|94;1|95;1|96;1|97;1|98;1|99;1|100;1|101;1|102;1|103;1|105;1|106;1|107;1|108;1|109;1|110;0|111;1|112;1|113;1|114;1|115;1|116;1|117;1|118;1|119;1|120;0|121;0|122;0|123;0|124;0|125;0|126;0|127;0|128;0|129;0|130;0|131;0|132;0|133;0|134;0|135;0|136;0|137;0|138;0|139;0|140;0|141;0|143;1|144;0|145;0|146;0|147;0|148;0|149;0|150;0|151;0|152;1|153;0|154;0|155;0|156;0|157;0|158;0|159;0|161;1|162;1|163;1|164;1|165;1|166;0|167;1|168;1|169;1|170;1|171;1|173;1|174;1|175;0|177;1|178;1|179;1|180;1|181;1|182;1|200;0|201;0|202;0|203;0|204;0|205;0|206;0|207;0|208;0|209;1|210;0|211;0|212;0|213;0|214;0|215;0|216;0|217;0|218;0|219;0|220;0|221;0|222;0|223;0|224;0|225;0|226;0|227;0|228;0|229;0|230;1|231;1|232;1|233;1|234;1|235;1|236;0|243;0|244;0|245;0|246;0|247;0|248;0|249;0|250;0|251;0|252;1|253;1|254;0|255;0|256;0|257;0|258;0|259;0|260;0|261;0|266;0|267;0|268;0|269;0|270;0|271;0|272;0|273;0|274;0|275;1|276;1|277;1|278;1|279;1|280;2|284;0|285;0|286;0|287;0|288;0|289;0|290;0|291;0|292;0|293;0|294;0|295;0|296;0|297;0|298;0|299;0|300;0|301;0|302;0|303;0|304;0|306;0|307;0|308;0|309;0|310;0|311;0|312;0|313;0|314;0|315;1|316;0|317;0|318;0|319;0|320;0|321;0|322;0|323;0|324;0|325;0|326;0|327;0|328;0|329;0|330;0|331;0|332;0|333;0|334;1|335;1|336;0|337;1|338;2|339;0|440;0|441;0|442;0|443;0|444;0|445;0|446;0|447;0|448;0|449;0|450;0|451;0|452;0|453;1|454;1|455;1|457;1|459;0|460;0|461;1|462;0|463;0|464;1|465;1|466;1|467;0|468;1|469;1|470;0|471;1|472;1|473;0|474;0|476;1|477;1|478;1|479;1|480;1|481;1|482;1|483;1|484;1|485;1|486;1|487;0|488;1|490;1|491;0|492;1|493;0|494;0|495;1|496;0|497;0|498;0|499;0|500;0|501;0|502;2|503;2|504;2|505;2|506;2|507;2|508;2|509;2|510;2|511;2|512;1|513;1|514;1|515;2|536;0|540;0|541;0|542;0|543;0|600;0|800;0|801;0|802;0|803;0|804;0|805;0|806;0|807;0|808;0|809;0|810;0|811;0|812;0|813;0|814;0|815;0|901;0|902;0|903;0|904;0|905;0|906;0|907;0|908;0|909;0|910;0|911;0|912;0|913;0|914;0|915;0|916;0|917;0|918;0|919;0|920;0|921;0|922;0|923;0|924;0|925;0|926;0|927;0|928;0|929;0|930;0|931;0|932;0|933;0|934;0|935;0|936;0|937;0|938;0|939;0|940;0|941;0|942;0|943;0|944;0|945;0|946;0|947;0|948;0|949;0|950;0|951;0|952;0|953;0|954;0|955;0|956;0|957;0|958;0|959;0|960;0|961;0|962;0|963;0|964;0|965;0|966;0|967;0|968;0|969;0|970;0|971;0|972;0|973;0|974;0|975;0|976;0|977;0|978;0|979;0|980;0|981;0|982;0|983;0|984;0|985;0|986;0|987;0|988;0|989;0|990;0|991;0|992;0|993;0|994;0|995;0|996;0|997;0|998;0|999;0|1000;0|1001;0|1002;0|1003;0|1004;0|1005;0|1006;0|1007;0|1008;0|1009;0|1010;0|1011;0|1012;0|1013;0|1014;0|1015;0|1016;0|1018;0|1019;0|1020;0|1021;0|1022;0|1023;0|1024;0|1025;0|1026;0|1027;0|1028;0|1029;0|1030;0|1031;0|1032;0|1033;0|1034;0|1035;0|1036;0|1037;0|1038;0|1039;0|1040;0|1041;0|1042;0|1043;0|1044;0|1045;0|1046;0|1047;0|1048;0|1049;0|1050;0|1051;0|1052;0|1053;0|1054;0|1055;0|1056;0|1057;0|1058;0|1059;0|1060;0|1200;0|1201;0|1202;0|1203;0|1204;0|1205;0|2000;0\0");
            prmClient.send("AR8200");

            prmClient.send(createAsPacket(prmClient));
        }

        public static string CreateStuffPacketOM(listenClient prmClient)
        {
            account.character.Character character = prmClient.account.character;
            StringBuilder packet = new StringBuilder("OS+5|");
            for (int i = 0; i < character.Invertaire.Stuff.Count; i++)
            {
                if (character.Invertaire.Stuff[i].Position != -1)
                {
                    packet.Append($"{character.Invertaire.Stuff[i].ID}|");
                }
            }
            return packet.ToString();
        }

        public static string createAsPacket(listenClient prmClient)
        {
            account.character.Character character = prmClient.account.character;
            StringBuilder packet = new StringBuilder();
            character.UpdateEquipentStats();
            packet.Append($"As{character.XP},{prmClient.database.experience.ExperienceStat[character.level][0]},{prmClient.database.experience.ExperienceStat[character.level+1][0]}|" +
                $"{character.kamas}|" +
                $"{character.capital}|" +
                $"{character.PSorts}|" +
                $"0|" +
                $"{character.vie},{character.TotalVie}|" +
                $"{character.energie},10000|" +
                $"10|" +
                $"10|" +
                $"{character.PA},{character.EquipementPA},0,0,{character.TotalPA}|" +
                $"{character.PM},{character.EquipementPM},0,0,{character.TotalPM}|" +
                $"{character.force},{character.EquipementForce},0,{character.TotalForce}|" +
                $"{character.vie},{character.EquipementVie},0,{character.TotalVie}|" +
                $"{character.sagesse},{character.EquipementSagesse},0,{character.TotalSagesse}|" +
                $"{character.chance},{character.EquipementChance},0,{character.TotalChance}|" +
                $"{character.agi},{character.EquipementAgi},0,{character.TotalAgi}|" +
                $"{character.intell},{character.EquipementIntell},0,{character.TotalIntell}|" +
                $"{character.PO},0,0,0|" +
                $"{character.invo},0,0,0|");
            for (int i = 0; i < 32; i++)
            {
                packet.Append("0,0,0,0,0|");
            }
            packet.Append("\0Ab");
            return packet.ToString();
        }

        [PacketAttribute("AS")]
        public void LoginInWorld(Network.listenClient prmClient, string prmPacket)
        {
            if (int.TryParse(prmPacket.Substring(2).Split('\0')[0], out int id))
            {
                foreach (account.character.Character character in prmClient.account.ListCharacter)
                {
                    if (id  == character.id)
                    {
                        prmClient.account.character = character;
                        prmClient.CharacterInWorld.Add(prmClient);
                        prmClient.send("Rx0");
                        prmClient.send(CreateASKPacket(character));
                        prmClient.send("ZS0");
                        CreateSpellPacket(prmClient);
                        prmClient.send($"Ow{character.pods}|{character.podsMax}\0Os");
                        prmClient.send("FO+\0Im189\0Im0152;2020~10~21~13~4~none\0Im0153;none");
                        prmClient.send("al|0;0|1;1|2;1|3;1|4;1|5;1|6;1|7;1|8;1|9;1|10;1|11;1|12;1|13;1|14;1|15;1|16;1|17;1|18;1|19;1|20;1|21;1|22;1|23;1|25;1|26;1|27;1|28;1|29;1|30;1|31;1|32;1|33;1|34;1|35;1|37;1|38;1|39;0|41;1|42;0|43;1|44;1|45;1|46;1|47;1|48;1|49;1|50;1|51;1");
                    }
                }
            }
            else          
                ListCharacter(prmClient, prmPacket);

        }

        public static string CreateASKPacket(Game.account.character.Character character)
        {
           // return "ASK|1257|Lorel|5|9|0|90|-1|-1|-1|2a5d5~9a9~1~~3d7#7e5#6e#92f,76#2#0#0#0d0+2,77#2#0#0#0d0+2,7b#2#0#0#0d0+2,7c#2#0#0#0d0+2,7e#2#0#0#0d0+2~0;2a5d6~9aa~1~~3d7#7e5#6e#92f,76#2#0#0#0d0+2,77#2#0#0#0d0+2,7b#2#0#0#0d0+2,7c#2#0#0#0d0+2,7e#2#0#0#0d0+2~0;2a5d7~9ab~1~~3d7#7e5#6e#92f,76#2#0#0#0d0+2,77#2#0#0#0d0+2,7b#2#0#0#0d0+2,7c#2#0#0#0d0+2,7e#2#0#0#0d0+2~0;2a5d8~9ac~1~~3d7#7e5#6e#92f,76#2#0#0#0d0+2,77#2#0#0#0d0+2,7b#2#0#0#0d0+2,7c#2#0#0#0d0+2,7e#2#0#0#0d0+2~0;2a5d9~9ad~1~~3d7#7e5#6e#92f,76#2#0#0#0d0+2,77#2#0#0#0d0+2,7b#2#0#0#0d0+2,7c#2#0#0#0d0+2,7e#2#0#0#0d0+2~0;2a5da~9ae~1~~3d7#7e5#6e#92f,76#2#0#0#0d0+2,77#2#0#0#0d0+2,7b#2#0#0#0d0+2,7c#2#0#0#0d0+2,7e#2#0#0#0d0+2~0;35b79~133~6~~~1;35b7a~2161~2~~~1;35b7b~173~2~~~10;35b7c~181~6~~~0;35b7d~374~9~~~0;35b7e~16058~1~~~0;35b7f~375~6~~~0;35b80~207~1~~~101;35b81~97c~1~~9e#11d#0#0#0d0+285~12; ";    
           
                
                StringBuilder packet = new StringBuilder($"ASK|{character.id}|{character.speudo}|{character.level}|{character.classe}|{character.sexe}|{character.gfxID}|{character.couleur1}|{character.couleur2}|{character.couleur3}|");

            for (int i = 0; i < character.Invertaire.Stuff.Count; i++)
            {

                packet.Append($"{character.Invertaire.Stuff[i].UID.ToString("X")}~{character.Invertaire.Stuff[i].ID.ToString("X")}~1~{character.Invertaire.Stuff[i].Position}~{character.Invertaire.Stuff[i].Stats};");
                
                    

            }
            return packet.ToString();
        }

        public static void CreateSpellPacket(listenClient prmClient)
        {
            prmClient.send($"SL{Game.Spell.SpellTemplate.CreatesSpellPacket(prmClient.account.character.SpellsCharacter)}\0eL7667711");
        }
        [PacketAttribute("ALf")]
        public void CancelCreateCharacter(Network.listenClient prmClient, string prmPacket)
        {
            prmClient.send("BN");
            send_character(prmClient);
        }

        private string send_character(listenClient prmClient)
        {
            string packet = "";
            foreach (account.character.Character charac in prmClient.account.ListCharacter)
            {
                packet += $"|{charac.id};{charac.speudo};{charac.level};{charac.gfxID};" +
                $"{charac.couleur1};{charac.couleur2};{charac.couleur3};null,null,null,null,null,1;0;{World.WorldConfig.ServerID};{charac.isDead};;200";

            }
            return $"ALK0|{prmClient.account.ListCharacter.Count}{packet}";
        }

    }
}
