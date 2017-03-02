/*
Navicat MySQL Data Transfer 
Target Server Type    : MYSQL
Target Server Version : 50173
File Encoding         : 65001 
Date: 2017-03-02 16:04:17
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for ApplicationMTR
-- ----------------------------
DROP TABLE IF EXISTS `ApplicationMTR`;
CREATE TABLE `ApplicationMTR` (
  `Id` varchar(255) NOT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `CategoryId` int(11) DEFAULT NULL,
  `CreateTime` datetime NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ApplicationMTR
-- ----------------------------
INSERT INTO `ApplicationMTR` VALUES ('3358681ac1724b7f90e079ac21e52c81', 'Book', '0', '0001-01-01 00:00:00');
INSERT INTO `ApplicationMTR` VALUES ('603e5f0ca7f643f7b22c9fb1b1a27cbd', 'Book', '0', '0001-01-01 00:00:00');
INSERT INTO `ApplicationMTR` VALUES ('61144c00602149acb40f07fa6cac110f', 'Game', '0', '0001-01-01 00:00:00');
INSERT INTO `ApplicationMTR` VALUES ('7d74240a47084868996dd609072df720', 'Work', '0', '0001-01-01 00:00:00');
INSERT INTO `ApplicationMTR` VALUES ('c33d813863c344678f1bbfaab0a04e36', 'Work', '0', '0001-01-01 00:00:00');
INSERT INTO `ApplicationMTR` VALUES ('d9d242dd923f4107b5f20eb113aa9e10', 'Game', '0', '0001-01-01 00:00:00');
INSERT INTO `ApplicationMTR` VALUES ('f7e40750c36b4711b8a8b8343daec01a', 'Test1', '0', '0001-01-01 00:00:00');

-- ----------------------------
-- Table structure for CategoryApplicationMTR
-- ----------------------------
DROP TABLE IF EXISTS `CategoryApplicationMTR`;
CREATE TABLE `CategoryApplicationMTR` (
  `Id` int(255) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CategoryApplicationMTR
-- ----------------------------
