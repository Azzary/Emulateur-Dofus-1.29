/*
 Navicat MySQL Data Transfer

 Source Server         : test
 Source Server Type    : MySQL
 Source Server Version : 50731
 Source Host           : localhost:3306
 Source Schema         : leafauth

 Target Server Type    : MySQL
 Target Server Version : 50731
 File Encoding         : 65001

 Date: 02/11/2020 20:05:50
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for account
-- ----------------------------
DROP TABLE IF EXISTS `account`;
CREATE TABLE `account`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(20) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `password` varchar(20) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `speudo` tinytext CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `isBan` tinyint(1) NULL DEFAULT 0,
  `role` int(11) NULL DEFAULT 0,
  `Qsecrete` varchar(20) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT 'dummy?',
  `Rsecrete` varchar(20) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT 'dummy',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 2 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of account
-- ----------------------------
INSERT INTO `account` VALUES (1, 'admin', 'admin', 'admin', 0, 0, 'dummy?', 'dummy');

-- ----------------------------
-- Table structure for server
-- ----------------------------
DROP TABLE IF EXISTS `server`;
CREATE TABLE `server`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `ip` varchar(20) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `port` int(11) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 603 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of server
-- ----------------------------
INSERT INTO `server` VALUES (602, '127.0.0.1', 5555);

SET FOREIGN_KEY_CHECKS = 1;
