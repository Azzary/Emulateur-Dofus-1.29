using LeafWorld.Game.Job.JobTemplate;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.World
{
    public class WorldConfig
    {
        public static readonly string IP = "127.0.0.1";
        public static readonly int ServerID = 602;
        public static readonly int PORT = 5555;

        public static int MAP_START_ENUTROF = 10299;
        public static int CELL_START_ENUTROF = 272;

        public static int MAP_START_FECA = 10300;
        public static int CELL_START_FECA = 321;


        public static int MAP_START_ECAFLIP = 10276;
        public static int CELL_START_ECAFLIP = 297;


        public static int MAP_START_SADIDAS = 10279;
        public static int CELL_START_SADIDAS = 255;


        public static int MAP_START_ENIRIPSA = 10283;
        public static int CELL_START_ENIRIPSA = 270;


        public static int MAP_START_OSAMODAS = 10285;
        public static int CELL_START_OSAMODAS = 219;


        public static int MAP_START_SRAM = 10285;
        public static int CELL_START_SRAM = 219;


        public static int MAP_START_PANDAWA = 10289;
        public static int CELL_START_PANDAWA = 249;


        public static int MAP_START_CRA = 10285;
        public static int CELL_START_CRA = 219;


        public static int MAP_START_IOP = 10294;
        public static int CELL_START_IOP = 235;


        public static int MAP_START_SACRIEUR = 10296;
        public static int CELL_START_SACRIEUR = 229;


        public static int MAP_START_XELOR = 10298;
        public static int CELL_START_XELOR = 286;

        public static int[] GetPosStart(int classe)
        {
            int[] info;
            switch (classe)
            {
                case 1:
                    info = new int [] {CELL_START_FECA,MAP_START_FECA};
                    break;
                case 2:
                    info = new int [] {CELL_START_OSAMODAS,MAP_START_OSAMODAS};
                    break;
                case 3:
                    info = new int [] {CELL_START_ENUTROF,MAP_START_ENUTROF};
                    break;
                case 4:
                    info = new int [] {CELL_START_SRAM,MAP_START_SRAM};
                    break;
                case 5:
                    info = new int [] {CELL_START_XELOR,MAP_START_XELOR};
                    break;
                case 6:
                    info = new int [] {CELL_START_ECAFLIP,MAP_START_ECAFLIP};
                    break;
                case 7:
                    info = new int [] {CELL_START_ENIRIPSA,MAP_START_ENIRIPSA};
                    break;
                case 8:
                    info = new int [] {CELL_START_IOP,MAP_START_IOP};
                    break;
                case 9:
                    info = new int [] {CELL_START_CRA,MAP_START_CRA};
                    break;
                case 10:
                    info = new int [] {CELL_START_SADIDAS,MAP_START_SADIDAS};
                    break;
                case 11:
                    info = new int [] {CELL_START_SACRIEUR,MAP_START_SACRIEUR};
                    break;
                case 12:
                    info = new int [] {CELL_START_PANDAWA,MAP_START_PANDAWA};
                    break;
                default:
                    info = new int[] { 0, 0 };
                    break;

            }

            return info;
        }
        public static Dictionary<JobIdEnum, int[]> JOB_TOOLS = new Dictionary<JobIdEnum, int[]>() {
            { JobIdEnum.JOB_BUCHERON, new int[] { 454, 8539, 1378, 2608, 478, 2593, 2592, 2600, 2604, 456, 502, 675, 674, 923, 927, 515, 782, 673, 676, 771 } },
            { JobIdEnum.JOB_PAYSAN, new int[] { 577, 765, 8127, 8540, 8992, 12006 } },
            { JobIdEnum.JOB_PECHEUR, new int[] { 596, 1860, 1863, 1865, 1866, 1867, 1868, 2188, 2366, 8541 } },
            { JobIdEnum.JOB_FORGEUR_EPEES, new int[] { 494 } },
            { JobIdEnum.JOB_MINEUR, new int[] { 497 } },
            { JobIdEnum.JOB_ALCHIMISTE, new int[] { 1473 } },
            { JobIdEnum.JOB_TAILLEUR, new int[] { 951 } },
            { JobIdEnum.JOB_BOULANGER, new int[] { 492 } },
            { JobIdEnum.JOB_SCULPTEUR_ARCS, new int[] { 500 } },
            { JobIdEnum.JOB_FORGEUR_DE_MARTEAUX, new int[] { 493 } },
            { JobIdEnum.JOB_FORGEUR_DE_BOUCLIERS, new int[] { 7098 } },
            { JobIdEnum.JOB_CORDONNIER, new int[] { 579 } },
            { JobIdEnum.JOB_BIJOUTIER, new int[] { 491 } },
            { JobIdEnum.JOB_SCULPTEUR_DE_BATONS, new int[] { 498 } },
            { JobIdEnum.JOB_SCULPTEUR_DE_BAGUETTES, new int[] { 499 } },
            { JobIdEnum.JOB_FORGEUR_DE_DAGUES, new int[] { 495 } },
            { JobIdEnum.JOB_FORGEUR_DE_PELLES, new int[] { 496 } },
            { JobIdEnum.JOB_FORGEUR_DE_HACHES, new int[] { 922 } },
            { JobIdEnum.JOB_BRICOLEUR, new int[] { 7650 } },
            { JobIdEnum.JOB_CHASSEUR, new int[] { } },
            { JobIdEnum.JOB_BOUCHER, new int[] { 1945 } },
            { JobIdEnum.JOB_POISSONNIER, new int[] { 1946 } },
        };
    }
}
