/*
 Navicat MySQL Data Transfer

 Source Server         : test
 Source Server Type    : MySQL
 Source Server Version : 50731
 Source Host           : localhost:3306
 Source Schema         : leafworld602

 Target Server Type    : MySQL
 Target Server Version : 50731
 File Encoding         : 65001

 Date: 02/11/2020 20:06:07
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for maptemplate
-- ----------------------------
DROP TABLE IF EXISTS `maptemplate`;
CREATE TABLE `maptemplate`  (
  `Id` int(11) NOT NULL,
  `CreateTime` varchar(150) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `Data` longtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `DataKey` longtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `Places` longtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `Width` int(11) NOT NULL,
  `Height` int(11) NOT NULL,
  `X` int(11) NOT NULL,
  `Y` int(11) NOT NULL,
  `Capabilities` int(11) NOT NULL DEFAULT 0,
  `SubAreaId` int(11) NOT NULL,
  PRIMARY KEY (`Id`, `CreateTime`) USING BTREE
) ENGINE = MyISAM CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for personnage
-- ----------------------------
DROP TABLE IF EXISTS `personnage`;
CREATE TABLE `personnage`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `accountid` int(11) NOT NULL,
  `level` int(11) NULL DEFAULT 1,
  `classe` int(11) NOT NULL,
  `sexe` int(11) NOT NULL,
  `speudo` tinytext CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `couleur1` int(11) NOT NULL,
  `couleur2` int(11) NOT NULL,
  `couleur3` int(11) NOT NULL,
  `pods` int(11) NOT NULL DEFAULT 0,
  `gfxID` int(11) NOT NULL,
  `isDead` int(11) NULL DEFAULT 0,
  `SubArea` int(11) NULL DEFAULT 7669,
  `mapID` int(11) NULL DEFAULT 10299,
  `cellID` int(11) NULL DEFAULT 299,
  `XP` int(11) NULL DEFAULT 0,
  `kamas` int(11) NULL DEFAULT 1000,
  `capital` int(11) NULL DEFAULT 0,
  `PSorts` int(11) NULL DEFAULT 0,
  `vie` int(11) NULL DEFAULT 50,
  `energie` int(11) NULL DEFAULT 10000,
  `PA` int(11) NULL DEFAULT 6,
  `PM` int(11) NULL DEFAULT 3,
  `forcee` int(11) NULL DEFAULT 0,
  `sagesse` int(11) NULL DEFAULT 0,
  `chance` int(11) NULL DEFAULT 0,
  `agi` int(11) NULL DEFAULT 0,
  `intell` int(11) NULL DEFAULT 0,
  `podsMax` int(11) NOT NULL DEFAULT 1000,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 3 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
