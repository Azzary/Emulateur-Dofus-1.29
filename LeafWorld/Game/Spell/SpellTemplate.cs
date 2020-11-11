using LeafWorld.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Game.Spell
{
    public class SpellTemplate
    {
        public static string CreatesSpellPacket(List<Game.Spell.Spell> listSpell)
        {
            byte i = 0;
            StringBuilder packet = new StringBuilder();
            foreach (Spell spell in listSpell)
            {
                packet.Append(spell.id);
                packet.Append('~');
                packet.Append(spell.level);
                packet.Append('~');
                packet.Append(i);
                packet.Append(';');
                i++;
            }
            return packet.ToString();
        }

        public static void AddSpell(Game.account.character.Character Character)
        {
            byte breed = Character.classe;
            int level = Character.level;
            switch (breed)
            {
                case 1:
                    if (level == 1)
                    {
                        Character.AddSpell( 3);
                        Character.AddSpell( 6);
                        Character.AddSpell( 17);
                        break;
                    }
                    if (level == 3)
                        Character.AddSpell( 4);//Renvoie de sort
                    if (level == 6)
                        Character.AddSpell( 2);//Aveuglement
                    if (level == 9)
                        Character.AddSpell( 1);//Armure Incandescente
                    if (level == 13)
                        Character.AddSpell( 9);//Attaque nuageuse
                    if (level == 17)
                        Character.AddSpell( 18);//Armure Aqueuse
                    if (level == 21)
                        Character.AddSpell( 20);//Immunit�
                    if (level == 26)
                        Character.AddSpell( 14);//Armure Venteuse
                    if (level == 31)
                        Character.AddSpell( 19);//Bulle
                    if (level == 36)
                        Character.AddSpell( 5);//Tr�ve
                    if (level == 42)
                        Character.AddSpell( 16);//Science du b�ton
                    if (level == 48)
                        Character.AddSpell( 8);//Retour du b�ton
                    if (level == 54)
                        Character.AddSpell( 12);//glyphe d'Aveuglement
                    if (level == 60)
                        Character.AddSpell( 11);//T�l�portation
                    if (level == 70)
                        Character.AddSpell( 10);//Glyphe Enflamm�
                    if (level == 80)
                        Character.AddSpell( 7);//Bouclier F�ca
                    if (level == 90)
                        Character.AddSpell( 15);//Glyphe d'Immobilisation
                    if (level == 100)
                        Character.AddSpell( 13);//Glyphe de Silence
                    if (level == 200)
                        Character.AddSpell( 1901);//Invocation de Dopeul F�ca
                    break;

                case 2:
                    if (level == 1)
                    {
                        Character.AddSpell( 34);
                        Character.AddSpell( 21);
                        Character.AddSpell( 23);
                        break;
                    }
                    if (level == 3)
                        Character.AddSpell( 26);//B�n�diction Animale
                    if (level == 6)
                        Character.AddSpell( 22);//D�placement F�lin
                    if (level == 9)
                        Character.AddSpell( 35);//Invocation de Bouftou
                    if (level == 13)
                        Character.AddSpell( 28);//Crapaud
                    if (level == 17)
                        Character.AddSpell( 37);//Invocation de Prespic
                    if (level == 21)
                        Character.AddSpell( 30);//Fouet
                    if (level == 26)
                        Character.AddSpell( 27);//Piq�re Motivante
                    if (level == 31)
                        Character.AddSpell( 24);//Corbeau
                    if (level == 36)
                        Character.AddSpell( 33);//Griffe Cinglante
                    if (level == 42)
                        Character.AddSpell( 25);//Soin Animal
                    if (level == 48)
                        Character.AddSpell( 38);//Invocation de Sanglier
                    if (level == 54)
                        Character.AddSpell( 36);//Frappe du Craqueleur
                    if (level == 60)
                        Character.AddSpell( 32);//R�sistance Naturelle
                    if (level == 70)
                        Character.AddSpell( 29);//Crocs du Mulou
                    if (level == 80)
                        Character.AddSpell( 39);//Invocation de Bwork Mage
                    if (level == 90)
                        Character.AddSpell( 40);//Invocation de Craqueleur
                    if (level == 100)
                        Character.AddSpell( 31);//Invocation de Dragonnet Rouge
                    if (level == 200)
                        Character.AddSpell( 1902);//Invocation de Dopeul Osamodas
                    break;

                case 3:
                    if (level == 1)
                    {
                        Character.AddSpell( 51);
                        Character.AddSpell( 43);
                        Character.AddSpell( 41);
                        break;
                    }
                    if (level == 3)
                        Character.AddSpell( 49);//Pelle Fantomatique
                    if (level == 6)
                        Character.AddSpell( 42);//Chance
                    if (level == 9)
                        Character.AddSpell( 47);//Bo�te de Pandore
                    if (level == 13)
                        Character.AddSpell( 48);//Remblai
                    if (level == 17)
                        Character.AddSpell( 45);//Cl� R�ductrice
                    if (level == 21)
                        Character.AddSpell( 53);//Force de l'Age
                    if (level == 26)
                        Character.AddSpell( 46);//D�sinvocation
                    if (level == 31)
                        Character.AddSpell( 52);//Cupidit�
                    if (level == 36)
                        Character.AddSpell( 44);//Roulage de Pelle
                    if (level == 42)
                        Character.AddSpell( 50);//Maladresse
                    if (level == 48)
                        Character.AddSpell( 54);//Maladresse de Masse
                    if (level == 54)
                        Character.AddSpell( 55);//Acc�l�ration
                    if (level == 60)
                        Character.AddSpell( 56);//Pelle du Jugement
                    if (level == 70)
                        Character.AddSpell( 58);//Pelle Massacrante
                    if (level == 80)
                        Character.AddSpell( 59);//Corruption
                    if (level == 90)
                        Character.AddSpell( 57);//Pelle Anim�e
                    if (level == 100)
                        Character.AddSpell( 60);//Coffre Anim�
                    if (level == 200)
                        Character.AddSpell( 1903);//Invocation de Dopeul Enutrof
                    break;

                case 4:
                    if (level == 1)
                    {
                        Character.AddSpell( 61);
                        Character.AddSpell( 72);
                        Character.AddSpell( 65);
                        break;
                    }
                    if (level == 3)
                        Character.AddSpell( 66);//Poison insidieux
                    if (level == 6)
                        Character.AddSpell( 68);//Fourvoiement
                    if (level == 9)
                        Character.AddSpell( 63);//Coup Sournois
                    if (level == 13)
                        Character.AddSpell( 74);//Double
                    if (level == 17)
                        Character.AddSpell( 64);//Rep�rage
                    if (level == 21)
                        Character.AddSpell( 79);//Pi�ge de Masse
                    if (level == 26)
                        Character.AddSpell( 78);//Invisibilit� d'Autrui
                    if (level == 31)
                        Character.AddSpell( 71);//Pi�ge Empoisonn�
                    if (level == 36)
                        Character.AddSpell( 62);//Concentration de Chakra
                    if (level == 42)
                        Character.AddSpell( 69);//Pi�ge d'Immobilisation
                    if (level == 48)
                        Character.AddSpell( 77);//Pi�ge de Silence
                    if (level == 54)
                        Character.AddSpell( 73);//Pi�ge r�pulsif
                    if (level == 60)
                        Character.AddSpell( 67);//Peur
                    if (level == 70)
                        Character.AddSpell( 70);//Arnaque
                    if (level == 80)
                        Character.AddSpell( 75);//Pulsion de Chakra
                    if (level == 90)
                        Character.AddSpell( 76);//Attaque Mortelle
                    if (level == 100)
                        Character.AddSpell( 80);//Pi�ge Mortel
                    if (level == 200)
                        Character.AddSpell( 1904);//Invocation de Dopeul Sram
                    break;

                case 5:
                    if (level == 1)
                    {
                        Character.AddSpell( 82);
                        Character.AddSpell( 81);
                        Character.AddSpell( 83);
                        break;
                    }
                    if (level == 3)
                        Character.AddSpell( 84);//Gelure
                    if (level == 6)
                        Character.AddSpell( 100);//Sablier de X�lor
                    if (level == 9)
                        Character.AddSpell( 92);//Rayon Obscur
                    if (level == 13)
                        Character.AddSpell( 88);//T�l�portation
                    if (level == 17)
                        Character.AddSpell( 93);//Fl�trissement
                    if (level == 21)
                        Character.AddSpell( 85);//Flou
                    if (level == 26)
                        Character.AddSpell( 96);//Poussi�re Temporelle
                    if (level == 31)
                        Character.AddSpell( 98);//Vol du Temps
                    if (level == 36)
                        Character.AddSpell( 86);//Aiguille Chercheuse
                    if (level == 42)
                        Character.AddSpell( 89);//D�vouement
                    if (level == 48)
                        Character.AddSpell( 90);//Fuite
                    if (level == 54)
                        Character.AddSpell( 87);//D�motivation
                    if (level == 60)
                        Character.AddSpell( 94);//Protection Aveuglante
                    if (level == 70)
                        Character.AddSpell( 99);//Momification
                    if (level == 80)
                        Character.AddSpell( 95);//Horloge
                    if (level == 90)
                        Character.AddSpell( 91);//Frappe de X�lor
                    if (level == 100)
                        Character.AddSpell( 97);//Cadran de X�lor
                    if (level == 200)
                        Character.AddSpell( 1905);//Invocation de Dopeul X�lor
                    break;

                case 6:
                    if (level == 1)
                    {
                        Character.AddSpell( 102);
                        Character.AddSpell( 103);
                        Character.AddSpell( 105);
                        break;
                    }
                    if (level == 3)
                        Character.AddSpell( 109);//Bluff
                    if (level == 6)
                        Character.AddSpell( 113);//Perception
                    if (level == 9)
                        Character.AddSpell( 111);//Contrecoup
                    if (level == 13)
                        Character.AddSpell( 104);//Tr�fle
                    if (level == 17)
                        Character.AddSpell( 119);//Tout ou rien
                    if (level == 21)
                        Character.AddSpell( 101);//Roulette
                    if (level == 26)
                        Character.AddSpell( 107);//Topkaj
                    if (level == 31)
                        Character.AddSpell( 116);//Langue R�peuse
                    if (level == 36)
                        Character.AddSpell( 106);//Roue de la Fortune
                    if (level == 42)
                        Character.AddSpell( 117);//Griffe Invocatrice
                    if (level == 48)
                        Character.AddSpell( 108);//Esprit F�lin
                    if (level == 54)
                        Character.AddSpell( 115);//Odorat
                    if (level == 60)
                        Character.AddSpell( 118);//R�flexes
                    if (level == 70)
                        Character.AddSpell( 110);//Griffe Joueuse
                    if (level == 80)
                        Character.AddSpell( 112);//Griffe de Ceangal
                    if (level == 90)
                        Character.AddSpell( 114);//Rekop
                    if (level == 100)
                        Character.AddSpell( 120);//Destin d'Ecaflip
                    if (level == 200)
                        Character.AddSpell( 1906);//Invocation de Dopeul Ecaflip
                    break;

                case 7:
                    if (level == 1)
                    {
                        Character.AddSpell( 125);
                        Character.AddSpell( 128);
                        Character.AddSpell( 121);
                        break;
                    }
                    if (level == 3)
                        Character.AddSpell( 124);//Mot Soignant
                    if (level == 6)
                        Character.AddSpell( 122);//Mot Blessant
                    if (level == 9)
                        Character.AddSpell( 126);//Mot Stimulant
                    if (level == 13)
                        Character.AddSpell( 127);//Mot de Pr�vention
                    if (level == 17)
                        Character.AddSpell( 123);//Mot Drainant
                    if (level == 21)
                        Character.AddSpell( 130);//Mot Revitalisant
                    if (level == 26)
                        Character.AddSpell( 131);//Mot de R�g�n�ration
                    if (level == 31)
                        Character.AddSpell( 132);//Mot d'Epine
                    if (level == 36)
                        Character.AddSpell( 133);//Mot de Jouvence
                    if (level == 42)
                        Character.AddSpell( 134);//Mot Vampirique
                    if (level == 48)
                        Character.AddSpell( 135);//Mot de Sacrifice
                    if (level == 54)
                        Character.AddSpell( 129);//Mot d'Amiti�
                    if (level == 60)
                        Character.AddSpell( 136);//Mot d'Immobilisation
                    if (level == 70)
                        Character.AddSpell( 137);//Mot d'Envol
                    if (level == 80)
                        Character.AddSpell( 138);//Mot de Silence
                    if (level == 90)
                        Character.AddSpell( 139);//Mot d'Altruisme
                    if (level == 100)
                        Character.AddSpell( 140);//Mot de Reconstitution
                    if (level == 200)
                        Character.AddSpell( 1907);//Invocation de Dopeul Eniripsa
                    break;

                case 8:
                    if (level == 1)
                    {
                        Character.AddSpell( 143);
                        Character.AddSpell( 141);
                        Character.AddSpell( 142);
                        break;
                    }
                    if (level == 3)
                        Character.AddSpell( 144);//Compulsion
                    if (level == 6)
                        Character.AddSpell( 145);//Ep�e Divine
                    if (level == 9)
                        Character.AddSpell( 146);//Ep�e du Destin
                    if (level == 13)
                        Character.AddSpell( 147);//Guide de Bravoure
                    if (level == 17)
                        Character.AddSpell( 148);//Amplification
                    if (level == 21)
                        Character.AddSpell( 154);//Ep�e Destructrice
                    if (level == 26)
                        Character.AddSpell( 150);//Couper
                    if (level == 31)
                        Character.AddSpell( 151);//Souffle
                    if (level == 36)
                        Character.AddSpell( 155);//Vitalit�
                    if (level == 42)
                        Character.AddSpell( 152);//Ep�e du Jugement
                    if (level == 48)
                        Character.AddSpell( 153);//Puissance
                    if (level == 54)
                        Character.AddSpell( 149);//Mutilation
                    if (level == 60)
                        Character.AddSpell( 156);//Temp�te de Puissance
                    if (level == 70)
                        Character.AddSpell( 157);//Ep�e C�leste
                    if (level == 80)
                        Character.AddSpell( 158);//Concentration
                    if (level == 90)
                        Character.AddSpell( 160);//Ep�e de Iop
                    if (level == 100)
                        Character.AddSpell( 159);//Col�re de Iop
                    if (level == 200)
                        Character.AddSpell( 1908);//Invocation de Dopeul Iop
                    break;

                case 9:
                    if (level == 1)
                    {
                        Character.AddSpell( 161);
                        Character.AddSpell( 169);
                        Character.AddSpell( 164);
                        break;
                    }
                    if (level == 3)
                        Character.AddSpell( 163);//Fl�che Glac�e
                    if (level == 6)
                        Character.AddSpell( 165);//Fl�che enflamm�e
                    if (level == 9)
                        Character.AddSpell( 172);//Tir Eloign�
                    if (level == 13)
                        Character.AddSpell( 167);//Fl�che d'Expiation
                    if (level == 17)
                        Character.AddSpell( 168);//Oeil de Taupe
                    if (level == 21)
                        Character.AddSpell( 162);//Tir Critique
                    if (level == 26)
                        Character.AddSpell( 170);//Fl�che d'Immobilisation
                    if (level == 31)
                        Character.AddSpell( 171);//Fl�che Punitive
                    if (level == 36)
                        Character.AddSpell( 166);//Tir Puissant
                    if (level == 42)
                        Character.AddSpell( 173);//Fl�che Harcelante
                    if (level == 48)
                        Character.AddSpell( 174);//Fl�che Cinglante
                    if (level == 54)
                        Character.AddSpell( 176);//Fl�che Pers�cutrice
                    if (level == 60)
                        Character.AddSpell( 175);//Fl�che Destructrice
                    if (level == 70)
                        Character.AddSpell( 178);//Fl�che Absorbante
                    if (level == 80)
                        Character.AddSpell( 177);//Fl�che Ralentissante
                    if (level == 90)
                        Character.AddSpell( 179);//Fl�che Explosive
                    if (level == 100)
                        Character.AddSpell( 180);//Ma�trise de l'Arc
                    if (level == 200)
                        Character.AddSpell( 1909);//Invocation de Dopeul Cra
                    break;

                case 10:
                    if (level == 1)
                    {
                        Character.AddSpell( 183);
                        Character.AddSpell( 200);
                        Character.AddSpell( 193);
                        break;
                    }
                    if (level == 3)
                        Character.AddSpell( 198);//Sacrifice Poupesque
                    if (level == 6)
                        Character.AddSpell( 195);//Larme
                    if (level == 9)
                        Character.AddSpell( 182);//Invocation de la Folle
                    if (level == 13)
                        Character.AddSpell( 192);//Ronce Apaisante
                    if (level == 17)
                        Character.AddSpell( 197);//Puissance Sylvestre
                    if (level == 21)
                        Character.AddSpell( 189);//Invocation de la Sacrifi�e
                    if (level == 26)
                        Character.AddSpell( 181);//Tremblement
                    if (level == 31)
                        Character.AddSpell( 199);//Connaissance des Poup�es
                    if (level == 36)
                        Character.AddSpell( 191);//Ronce Multiples
                    if (level == 42)
                        Character.AddSpell( 186);//Arbre
                    if (level == 48)
                        Character.AddSpell( 196);//Vent Empoisonn�
                    if (level == 54)
                        Character.AddSpell( 190);//Invocation de la Gonflable
                    if (level == 60)
                        Character.AddSpell( 194);//Ronces Agressives
                    if (level == 70)
                        Character.AddSpell( 185);//Herbe Folle
                    if (level == 80)
                        Character.AddSpell( 184);//Feu de Brousse
                    if (level == 90)
                        Character.AddSpell( 188);//Ronce Insolente
                    if (level == 100)
                        Character.AddSpell( 187);//Invocation de la Surpuissante
                    if (level == 200)
                        Character.AddSpell( 1910);//Invocation de Dopeul Sadida
                    break;

                case 11:
                    if (level == 1)
                    {
                        Character.AddSpell( 432);
                        Character.AddSpell( 431);
                        Character.AddSpell( 434);
                        break;
                    }
                    if (level == 3)
                        Character.AddSpell( 444);//D�robade
                    if (level == 6)
                        Character.AddSpell( 449);//D�tour
                    if (level == 9)
                        Character.AddSpell( 436);//Assaut
                    if (level == 13)
                        Character.AddSpell( 437);//Ch�timent Agile
                    if (level == 17)
                        Character.AddSpell( 439);//Dissolution
                    if (level == 21)
                        Character.AddSpell( 433);//Ch�timent Os�
                    if (level == 26)
                        Character.AddSpell( 443);//Ch�timent Spirituel
                    if (level == 31)
                        Character.AddSpell( 440);//Sacrifice
                    if (level == 36)
                        Character.AddSpell( 442);//Absorption
                    if (level == 42)
                        Character.AddSpell( 441);//Ch�timent Vilatesque
                    if (level == 48)
                        Character.AddSpell( 445);//Coop�ration
                    if (level == 54)
                        Character.AddSpell( 438);//Transposition
                    if (level == 60)
                        Character.AddSpell( 446);//Punition
                    if (level == 70)
                        Character.AddSpell( 447);//Furie
                    if (level == 80)
                        Character.AddSpell( 448);//Ep�e Volante
                    if (level == 90)
                        Character.AddSpell( 435);//Tansfert de Vie
                    if (level == 100)
                        Character.AddSpell( 450);//Folie Sanguinaire
                    if (level == 200)
                        Character.AddSpell( 1911);//Invocation de Dopeul Sacrieur
                    break;

                case 12:
                    if (level == 1)
                    {
                        Character.AddSpell( 686);
                        Character.AddSpell( 692);
                        Character.AddSpell( 687);
                        break;
                    }
                    if (level == 3)
                        Character.AddSpell( 689);//Epouvante
                    if (level == 6)
                        Character.AddSpell( 690);//Souffle Alcoolis�
                    if (level == 9)
                        Character.AddSpell( 691);//Vuln�rabilit� Aqueuse
                    if (level == 13)
                        Character.AddSpell( 688);//Vuln�rabilit� Incandescente
                    if (level == 17)
                        Character.AddSpell( 693);//Karcham
                    if (level == 21)
                        Character.AddSpell( 694);//Vuln�rabilit� Venteuse
                    if (level == 26)
                        Character.AddSpell( 695);//Stabilisation
                    if (level == 31)
                        Character.AddSpell( 696);//Chamrak
                    if (level == 36)
                        Character.AddSpell( 697);//Vuln�rabilit� Terrestre
                    if (level == 42)
                        Character.AddSpell( 698);//Souillure
                    if (level == 48)
                        Character.AddSpell( 699);//Lait de Bambou
                    if (level == 54)
                        Character.AddSpell( 700);//Vague � Lame
                    if (level == 60)
                        Character.AddSpell( 701);//Col�re de Zato�shwan
                    if (level == 70)
                        Character.AddSpell( 702);//Flasque Explosive
                    if (level == 80)
                        Character.AddSpell( 703);//Pandatak
                    if (level == 90)
                        Character.AddSpell( 704);//Pandanlku
                    if (level == 100)
                        Character.AddSpell( 705);//Lien Spiritueux
                    if (level == 200)
                        Character.AddSpell( 1912);//Invocation de Dopeul Pandawa
                    break;
            }
        }

    }
}
