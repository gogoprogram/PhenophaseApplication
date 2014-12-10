CREATE DATABASE  IF NOT EXISTS `phenophase` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `phenophase`;
-- MySQL dump 10.13  Distrib 5.6.17, for Win32 (x86)
--
-- Host: localhost    Database: test_phenophase
-- ------------------------------------------------------
-- Server version	5.6.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `contact_info`
--

DROP TABLE IF EXISTS `contact_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `contact_info` (
  `CONTACT_ID` varchar(3) NOT NULL,
  `CONTACT_NAME` varchar(60) DEFAULT NULL,
  `CONTACT_ADDRESS` varchar(100) DEFAULT NULL,
  `CONTACT_EMAIL` varchar(50) DEFAULT NULL,
  `CONTACT_PHONE` varchar(12) DEFAULT NULL,
  `CONTACT_EMPLOYER` varchar(50) DEFAULT NULL,
  `DATA_PROVIDED` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`CONTACT_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `date_doy`
--

DROP TABLE IF EXISTS `date_doy`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `date_doy` (
  `DATE` date NOT NULL,
  `DAY_OF_YEAR` smallint(3) DEFAULT NULL,
  PRIMARY KEY (`DATE`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ds_pheno`
--

DROP TABLE IF EXISTS `ds_pheno`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ds_pheno` (
  `PLANT_ID` varchar(8) NOT NULL,
  `DATE` date NOT NULL,
  `DS_01` tinyint(4) DEFAULT '-99',
  `DS_02` tinyint(4) DEFAULT '-99',
  `DS_202` smallint(6) DEFAULT '-99',
  `DS_03` tinyint(4) DEFAULT '-99',
  `DS_04` tinyint(4) DEFAULT '-99',
  `DS_05` tinyint(4) DEFAULT '-99',
  `DS_06` tinyint(4) DEFAULT '-99',
  `DS_07` tinyint(4) DEFAULT '-99',
  `DS_207` smallint(6) DEFAULT '-99',
  `DS_08` tinyint(4) DEFAULT '-99',
  `DS_208` smallint(6) DEFAULT '-99',
  `DS_09` tinyint(4) DEFAULT '-99',
  `DS_209` smallint(6) DEFAULT '-99',
  `DS_10` tinyint(4) DEFAULT '-99',
  `DS_210` smallint(6) DEFAULT '-99',
  `DS_11` tinyint(4) DEFAULT '-99',
  `DS_211` smallint(6) DEFAULT '-99',
  `DS_12` tinyint(4) DEFAULT '-99',
  `DS_213` tinyint(4) DEFAULT '-99',
  `DS_214` smallint(6) DEFAULT '-99',
  `NOTES_FLAG` tinyint(1) DEFAULT NULL,
  `PHOTO_FLAG` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`PLANT_ID`,`DATE`),
  KEY `FK_DS_DATE_idx` (`DATE`),
  CONSTRAINT `FK_DS_DATE` FOREIGN KEY (`DATE`) REFERENCES `date_doy` (`DATE`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_DS_PLANT_ID` FOREIGN KEY (`PLANT_ID`) REFERENCES `focal_plant_info` (`PLANT_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `es_pheno`
--

DROP TABLE IF EXISTS `es_pheno`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `es_pheno` (
  `PLANT_ID` varchar(8) NOT NULL,
  `DATE` date NOT NULL,
  `BE_01` tinyint(4) DEFAULT '-99',
  `BE_02` tinyint(4) DEFAULT '-99',
  `BE_03` tinyint(4) DEFAULT '-99',
  `BE_203` smallint(6) DEFAULT '-99',
  `BE_04` tinyint(4) DEFAULT '-99',
  `BE_204` smallint(6) DEFAULT '-99',
  `BE_05` tinyint(4) DEFAULT '-99',
  `BE_205` smallint(6) DEFAULT '-99',
  `BE_06` tinyint(4) DEFAULT '-99',
  `BE_206` smallint(6) DEFAULT '-99',
  `BE_07` tinyint(4) DEFAULT '-99',
  `BE_207` smallint(6) DEFAULT '-99',
  `NOTES_FLAG` tinyint(1) DEFAULT NULL,
  `PHOTO_FLAG` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`PLANT_ID`,`DATE`),
  KEY `FK_ES_DATE_idx` (`DATE`),
  CONSTRAINT `FK_ES_DATE` FOREIGN KEY (`DATE`) REFERENCES `date_doy` (`DATE`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ES_PLANT_ID` FOREIGN KEY (`PLANT_ID`) REFERENCES `focal_plant_info` (`PLANT_ID`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `focal_plant_info`
--

DROP TABLE IF EXISTS `focal_plant_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `focal_plant_info` (
  `PLANT_ID` varchar(8) NOT NULL,
  `SITE_CODE` varchar(2) NOT NULL,
  `SPP_CODE` varchar(4) NOT NULL,
  `FUNC_GRP_CODE` varchar(2) NOT NULL,
  `CANOPY_HT` float DEFAULT NULL,
  `CANOPY_DIAM1` float DEFAULT NULL,
  `CANOPY_DIAM2` float DEFAULT NULL,
  `CANOPY_AREA` double(7,4) DEFAULT NULL,
  `DIST_TO_DRAIN` smallint(6) DEFAULT NULL,
  `NOTES` varchar(300) DEFAULT NULL,
  PRIMARY KEY (`PLANT_ID`),
  KEY `SITE_CODE_idx` (`SITE_CODE`),
  KEY `FUNC_GRP_idx` (`FUNC_GRP_CODE`),
  KEY `FK_SPP_CODE` (`SPP_CODE`,`FUNC_GRP_CODE`),
  CONSTRAINT `FK_SPP_CODE` FOREIGN KEY (`SPP_CODE`) REFERENCES `species_info` (`SPP_CODE`) ON UPDATE CASCADE,
  CONSTRAINT `SITE_CODE` FOREIGN KEY (`SITE_CODE`) REFERENCES `site_info` (`SITE_CODE`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `location_info`
--

DROP TABLE IF EXISTS `location_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `location_info` (
  `LOCATION_CODE` varchar(4) NOT NULL,
  `LOCATION_NAME` varchar(60) DEFAULT NULL,
  `SITE_CODE` varchar(45) DEFAULT NULL,
  `MLRA` varchar(45) DEFAULT NULL,
  `STATE` varchar(35) DEFAULT NULL,
  `LOCATION_LAT` double(10,6) DEFAULT NULL,
  `LOCATION_LONG` double(10,6) DEFAULT NULL,
  PRIMARY KEY (`LOCATION_CODE`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `observer_info`
--

DROP TABLE IF EXISTS `observer_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `observer_info` (
  `OBSERVER_ID` varchar(3) NOT NULL,
  `OBSERVER_NAME` varchar(60) DEFAULT NULL,
  `OBSERVER_ADDRESS` varchar(100) DEFAULT NULL,
  `OBSERVER_EMAIL` varchar(50) DEFAULT NULL,
  `OBSERVER_PHONE` varchar(12) DEFAULT NULL,
  PRIMARY KEY (`OBSERVER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `pg_pheno`
--

DROP TABLE IF EXISTS `pg_pheno`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pg_pheno` (
  `PLANT_ID` varchar(8) NOT NULL,
  `DATE` date NOT NULL,
  `GR_01` tinyint(4) DEFAULT '-99',
  `GR_02` tinyint(4) DEFAULT '-99',
  `GR_202` smallint(6) DEFAULT '-99',
  `GR_03` tinyint(4) DEFAULT '-99',
  `GR_04` tinyint(4) DEFAULT '-99',
  `GR_204` smallint(6) DEFAULT '-99',
  `GR_05` tinyint(4) DEFAULT '-99',
  `GR_205` smallint(6) DEFAULT '-99',
  `GR_06` tinyint(4) DEFAULT '-99',
  `GR_206` smallint(6) DEFAULT '-99',
  `GR_07` tinyint(4) DEFAULT '-99',
  `GR_207` smallint(6) DEFAULT '-99',
  `GR_08` tinyint(4) DEFAULT '-99',
  `GR_09` tinyint(4) DEFAULT '-99',
  `GR_209` smallint(6) DEFAULT '-99',
  `NOTES_FLAG` tinyint(1) DEFAULT NULL,
  `PHOTO_FLAG` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`PLANT_ID`,`DATE`),
  KEY `FK_PG_DATE_idx` (`DATE`),
  CONSTRAINT `FK_PG_DATE` FOREIGN KEY (`DATE`) REFERENCES `date_doy` (`DATE`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_PG_PLANT_ID` FOREIGN KEY (`PLANT_ID`) REFERENCES `focal_plant_info` (`PLANT_ID`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `pheno_domain_time`
--

DROP TABLE IF EXISTS `pheno_domain_time`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pheno_domain_time` (
  `ATTRIBUTE_ID` int(11) NOT NULL,
  `DEFINITION_START_DATE` date DEFAULT NULL,
  `DEFINITION_END_DATE` date DEFAULT NULL,
  `DATA_DOMAIN_VALUE` varchar(500) DEFAULT NULL,
  `DOMAIN_VALUE_DESCRIPTION` varchar(500) DEFAULT NULL,
  `NOTES` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`ATTRIBUTE_ID`),
  CONSTRAINT `FK_PHT_ATTRIBUTE_ID` FOREIGN KEY (`ATTRIBUTE_ID`) REFERENCES `pheno_metadata` (`ATTRIBUTE_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `pheno_metadata`
--

DROP TABLE IF EXISTS `pheno_metadata`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pheno_metadata` (
  `ATTRIBUTE_ID` int(11) NOT NULL,
  `ATTRIBUTE_NAME` varchar(150) NOT NULL,
  `ATTRIBUTE_DEFINITION` varchar(300) DEFAULT NULL,
  `ATTRIBUTE_DATA_TYPE` varchar(45) DEFAULT NULL,
  `NULL_VALUE` varchar(45) DEFAULT NULL,
  `DESCRIPTION` varchar(600) DEFAULT NULL,
  PRIMARY KEY (`ATTRIBUTE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `pheno_title_info`
--

DROP TABLE IF EXISTS `pheno_title_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pheno_title_info` (
  `TITLE_ID` int(11) NOT NULL,
  `FUNC_GRP_CODE` varchar(2) NOT NULL,
  `TITLE_NAME` varchar(6) DEFAULT NULL,
  `TITLE_CATEGORY` varchar(45) DEFAULT NULL,
  `TITLE_DISPLAY_NAME` varchar(100) DEFAULT NULL,
  `TITLE_DESCRIPTION` varchar(300) DEFAULT NULL,
  `IS_ACTIVE` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`TITLE_ID`),
  KEY `FK_PSPE_FNGP_idx` (`FUNC_GRP_CODE`),
  KEY `FK_PSPE_SPC_idx` (`FUNC_GRP_CODE`),
  CONSTRAINT `FK_PTI_TITLE_ID` FOREIGN KEY (`TITLE_ID`) REFERENCES `pheno_metadata` (`ATTRIBUTE_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `photo_info`
--

DROP TABLE IF EXISTS `photo_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `photo_info` (
  `PLANT_ID` varchar(8) NOT NULL,
  `DATE` date NOT NULL,
  `PHOTO_NAME` varchar(100) NOT NULL,
  PRIMARY KEY (`PLANT_ID`,`DATE`,`PHOTO_NAME`),
  KEY `FK_PI_DATE_idx` (`DATE`),
  CONSTRAINT `FK_PI_DATE` FOREIGN KEY (`DATE`) REFERENCES `date_doy` (`DATE`) ON UPDATE CASCADE,
  CONSTRAINT `FK_PI_PLANT_ID` FOREIGN KEY (`PLANT_ID`) REFERENCES `focal_plant_info` (`PLANT_ID`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `plant_death_info`
--

DROP TABLE IF EXISTS `plant_death_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `plant_death_info` (
  `PLANT_ID` varchar(8) NOT NULL,
  `DEATH_DATE` date DEFAULT NULL,
  `DEATH_REASON` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`PLANT_ID`),
  CONSTRAINT `FK_PDI_PLANT_ID` FOREIGN KEY (`PLANT_ID`) REFERENCES `focal_plant_info` (`PLANT_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `plant_note`
--

DROP TABLE IF EXISTS `plant_note`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `plant_note` (
  `PLANT_ID` varchar(8) NOT NULL,
  `DATE` date NOT NULL,
  `PLANT_NOTES` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`PLANT_ID`,`DATE`),
  CONSTRAINT `FK_N_PLANT_ID` FOREIGN KEY (`PLANT_ID`) REFERENCES `focal_plant_info` (`PLANT_ID`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `site_info`
--

DROP TABLE IF EXISTS `site_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `site_info` (
  `SITE_CODE` varchar(2) NOT NULL,
  `SITE_NAME` varchar(15) NOT NULL,
  `SOIL_TYPE` varchar(100) DEFAULT NULL,
  `UTMX` int(11) DEFAULT NULL,
  `UTMY` int(11) DEFAULT NULL,
  `SITE_LAT` double(10,6) DEFAULT NULL,
  `SITE_LONG` double(10,6) DEFAULT NULL,
  `ECOL_SITE` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`SITE_CODE`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `site_note`
--

DROP TABLE IF EXISTS `site_note`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `site_note` (
  `DATE` date NOT NULL,
  `SITE_CODE` varchar(2) NOT NULL,
  `SITE_NOTES` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`DATE`,`SITE_CODE`),
  KEY `FK_SN_SITE_CODE_idx` (`SITE_CODE`),
  CONSTRAINT `FK_SN_DATE` FOREIGN KEY (`DATE`) REFERENCES `date_doy` (`DATE`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_SN_SITE_CODE` FOREIGN KEY (`SITE_CODE`) REFERENCES `site_info` (`SITE_CODE`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `site_visit`
--

DROP TABLE IF EXISTS `site_visit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `site_visit` (
  `DATE` date NOT NULL,
  `SITE_CODE` varchar(2) NOT NULL,
  `OBSERVER_ID` varchar(3) NOT NULL,
  PRIMARY KEY (`DATE`,`SITE_CODE`,`OBSERVER_ID`),
  KEY `SITE_CODE_idx` (`SITE_CODE`),
  KEY `FK_OBSERVER_ID_idx` (`OBSERVER_ID`),
  CONSTRAINT `FK_SV_DATE` FOREIGN KEY (`DATE`) REFERENCES `date_doy` (`DATE`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_SV_OBSERVER_ID` FOREIGN KEY (`OBSERVER_ID`) REFERENCES `observer_info` (`OBSERVER_ID`) ON UPDATE CASCADE,
  CONSTRAINT `FK_SV_SITE_CODE` FOREIGN KEY (`SITE_CODE`) REFERENCES `site_info` (`SITE_CODE`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `species_info`
--

DROP TABLE IF EXISTS `species_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `species_info` (
  `SPP_CODE` varchar(4) NOT NULL,
  `FUNC_GRP_CODE` varchar(2) NOT NULL,
  `USDA_SYMBOL` varchar(8) DEFAULT NULL,
  `GROWTH_HABIT` varchar(30) DEFAULT NULL,
  `GROWTH_FORM` varchar(45) DEFAULT NULL,
  `SCIENTIFIC_NAME` varchar(100) DEFAULT NULL,
  `COMMON_NAME` varchar(100) DEFAULT NULL,
  `GENUS` varchar(60) DEFAULT NULL,
  `FUNC_GRP` varchar(45) DEFAULT NULL,
  `FAMILY` varchar(100) DEFAULT NULL,
  `DURATION` varchar(60) DEFAULT NULL,
  `PHOTO_SYN` varchar(30) DEFAULT NULL,
  `USDA_URL` varchar(350) DEFAULT NULL,
  PRIMARY KEY (`SPP_CODE`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `su_pheno`
--

DROP TABLE IF EXISTS `su_pheno`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `su_pheno` (
  `PLANT_ID` varchar(8) NOT NULL,
  `DATE` date NOT NULL,
  `CA_01` tinyint(4) DEFAULT '-99',
  `CA_201` smallint(6) DEFAULT '-99',
  `CA_02` tinyint(4) DEFAULT '-99',
  `CA_202` smallint(6) DEFAULT '-99',
  `CA_03` tinyint(4) DEFAULT '-99',
  `CA_203` smallint(6) DEFAULT '-99',
  `CA_04` tinyint(4) DEFAULT '-99',
  `CA_204` smallint(6) DEFAULT '-99',
  `CA_05` tinyint(4) DEFAULT '-99',
  `CA_205` smallint(6) DEFAULT '-99',
  `NOTES_FLAG` tinyint(1) DEFAULT NULL,
  `PHOTO_FLAG` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`PLANT_ID`,`DATE`),
  KEY `DATE_idx` (`DATE`),
  CONSTRAINT `FK_SU_DATE` FOREIGN KEY (`DATE`) REFERENCES `date_doy` (`DATE`) ON UPDATE CASCADE,
  CONSTRAINT `FK_SU_PLANT_ID` FOREIGN KEY (`PLANT_ID`) REFERENCES `focal_plant_info` (`PLANT_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `table_attribute`
--

DROP TABLE IF EXISTS `table_attribute`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `table_attribute` (
  `ATTRIBUTE_ID` int(11) NOT NULL,
  `TABLE_NAME` varchar(50) NOT NULL,
  PRIMARY KEY (`ATTRIBUTE_ID`,`TABLE_NAME`),
  CONSTRAINT `FK_TA_ATTRIBUTE_ID` FOREIGN KEY (`ATTRIBUTE_ID`) REFERENCES `pheno_metadata` (`ATTRIBUTE_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `temp_table`
--

DROP TABLE IF EXISTS `temp_table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `temp_table` (
  `seq` int(11) NOT NULL AUTO_INCREMENT,
  `obsDate` date NOT NULL,
  `plantId` varchar(8) NOT NULL,
  `phnTitle` tinyint(4) DEFAULT NULL,
  `phnIntensity` smallint(6) DEFAULT NULL,
  PRIMARY KEY (`seq`)
) ENGINE=InnoDB AUTO_INCREMENT=256 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Temporary table structure for view `vw_ds`
--

DROP TABLE IF EXISTS `vw_ds`;
/*!50001 DROP VIEW IF EXISTS `vw_ds`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `vw_ds` (
  `Date` tinyint NOT NULL,
  `JulianDay` tinyint NOT NULL,
  `Site` tinyint NOT NULL,
  `PlantID` tinyint NOT NULL,
  `SpeciesCode` tinyint NOT NULL,
  `FuncGrpCode` tinyint NOT NULL,
  `ObserverID` tinyint NOT NULL,
  `SiteNotes` tinyint NOT NULL,
  `DS_01` tinyint NOT NULL,
  `DS_02` tinyint NOT NULL,
  `DS_202` tinyint NOT NULL,
  `DS_03` tinyint NOT NULL,
  `DS_04` tinyint NOT NULL,
  `DS_05` tinyint NOT NULL,
  `DS_06` tinyint NOT NULL,
  `DS_213` tinyint NOT NULL,
  `DS_214` tinyint NOT NULL,
  `DS_07` tinyint NOT NULL,
  `DS_207` tinyint NOT NULL,
  `DS_08` tinyint NOT NULL,
  `DS_208` tinyint NOT NULL,
  `DS_09` tinyint NOT NULL,
  `DS_209` tinyint NOT NULL,
  `DS_10` tinyint NOT NULL,
  `DS_210` tinyint NOT NULL,
  `DS_11` tinyint NOT NULL,
  `DS_211` tinyint NOT NULL,
  `DS_12` tinyint NOT NULL,
  `PlantNotes` tinyint NOT NULL,
  `PhotoName` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Temporary table structure for view `vw_es`
--

DROP TABLE IF EXISTS `vw_es`;
/*!50001 DROP VIEW IF EXISTS `vw_es`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `vw_es` (
  `Date` tinyint NOT NULL,
  `JulianDay` tinyint NOT NULL,
  `Site` tinyint NOT NULL,
  `PlantID` tinyint NOT NULL,
  `SpeciesCode` tinyint NOT NULL,
  `FuncGrpCode` tinyint NOT NULL,
  `ObserverID` tinyint NOT NULL,
  `SiteNotes` tinyint NOT NULL,
  `BE_01` tinyint NOT NULL,
  `BE_02` tinyint NOT NULL,
  `BE_03` tinyint NOT NULL,
  `BE_203` tinyint NOT NULL,
  `BE_04` tinyint NOT NULL,
  `BE_204` tinyint NOT NULL,
  `BE_05` tinyint NOT NULL,
  `BE_205` tinyint NOT NULL,
  `BE_06` tinyint NOT NULL,
  `BE_206` tinyint NOT NULL,
  `BE_07` tinyint NOT NULL,
  `BE_207` tinyint NOT NULL,
  `PlantNotes` tinyint NOT NULL,
  `PhotoName` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Temporary table structure for view `vw_pg`
--

DROP TABLE IF EXISTS `vw_pg`;
/*!50001 DROP VIEW IF EXISTS `vw_pg`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `vw_pg` (
  `Date` tinyint NOT NULL,
  `JulianDay` tinyint NOT NULL,
  `Site` tinyint NOT NULL,
  `PlantID` tinyint NOT NULL,
  `SpeciesCode` tinyint NOT NULL,
  `FuncGrpCode` tinyint NOT NULL,
  `ObserverID` tinyint NOT NULL,
  `SiteNotes` tinyint NOT NULL,
  `GR_01` tinyint NOT NULL,
  `GR_02` tinyint NOT NULL,
  `GR_202` tinyint NOT NULL,
  `GR_03` tinyint NOT NULL,
  `GR_04` tinyint NOT NULL,
  `GR_204` tinyint NOT NULL,
  `GR_05` tinyint NOT NULL,
  `GR_205` tinyint NOT NULL,
  `GR_06` tinyint NOT NULL,
  `GR_206` tinyint NOT NULL,
  `GR_07` tinyint NOT NULL,
  `GR_207` tinyint NOT NULL,
  `GR_08` tinyint NOT NULL,
  `GR_09` tinyint NOT NULL,
  `GR_209` tinyint NOT NULL,
  `PlantNotes` tinyint NOT NULL,
  `PhotoName` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Temporary table structure for view `vw_su`
--

DROP TABLE IF EXISTS `vw_su`;
/*!50001 DROP VIEW IF EXISTS `vw_su`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `vw_su` (
  `Date` tinyint NOT NULL,
  `JulianDay` tinyint NOT NULL,
  `Site` tinyint NOT NULL,
  `PlantID` tinyint NOT NULL,
  `SpeciesCode` tinyint NOT NULL,
  `FuncGrpCode` tinyint NOT NULL,
  `ObserverID` tinyint NOT NULL,
  `SiteNotes` tinyint NOT NULL,
  `CA_01` tinyint NOT NULL,
  `CA_201` tinyint NOT NULL,
  `CA_02` tinyint NOT NULL,
  `CA_202` tinyint NOT NULL,
  `CA_03` tinyint NOT NULL,
  `CA_203` tinyint NOT NULL,
  `CA_04` tinyint NOT NULL,
  `CA_204` tinyint NOT NULL,
  `CA_05` tinyint NOT NULL,
  `CA_205` tinyint NOT NULL,
  `PlantNotes` tinyint NOT NULL,
  `PhotoName` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Dumping routines for database 'test_phenophase'
--
/*!50003 DROP PROCEDURE IF EXISTS `sp_geteventdates` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE PROCEDURE `sp_geteventdates`(IN plant_id varchar(10), IN syears varchar(500))
BEGIN
DROP TEMPORARY TABLE IF EXISTS eventTMP;

CREATE TEMPORARY TABLE IF NOT EXISTS eventTMP(
    seq1 INT NOT NULL,
	seq2 INT NOT NULL,
    plantId varchar(8) NOT NULL,
    date1 DATE,
	date2 DATE,
	ptitle1 tinyint,
    ptitle2 tinyint,
    diff tinyint
) ENGINE=MEMORY;
 
SET @sql = CONCAT('
INSERT INTO eventTMP(
SELECT tb1.seq,  tb2.seq, tb1.plantId, tb1.obsDate, tb2.obsDate,
tb1.phnTitle, tb2.phnTitle,
tb1.phnTitle-tb2.phnTitle
FROM temp_table AS tb1, temp_table AS tb2
WHERE tb1.seq+1 = tb2.seq 
AND year(tb1.obsDate) IN (',syears,') AND tb1.plantId = \'',plant_id,'\' 
AND year(tb1.obsDate) = year(tb2.obsDate) AND tb1.plantId = tb2.plantId
)');

PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SELECT plantId, DATE_FORMAT(date2, '%m-%d-%Y') FROM eventTMP where diff = -1
AND ptitle2 != 2; /*Added later to avoid dates when data was not recorded*/

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_get_abundanceresult` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE PROCEDURE `sp_get_abundanceresult`(IN fg_tbl varchar(10), IN spc_code_lst varchar(500),
 IN st_code varchar(500), IN ph_title varchar(6), IN ph_abnd varchar(6), IN syear varchar(500))
BEGIN
DECLARE pid varchar(8);
DECLARE phsdate date;
DECLARE phedate date;
DECLARE finished_cur1 tinyint DEFAULT 0;
DECLARE finished_cur2 tinyint DEFAULT 0;


DECLARE cur1 CURSOR FOR (SELECT plantId, date2 FROM eventTMP WHERE diff = -1
GROUP BY plantId, date2);

DECLARE cur2 CURSOR FOR (SELECT plantId, date1 FROM eventTMP WHERE diff = 1
GROUP BY plantId, date1);

-- DECLARE CONTINUE HANDLER FOR NOT FOUND SET finished_cur1 = 1;
-- DECLARE CONTINUE HANDLER FOR NOT FOUND SET finished_cur2 = 1;
DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET finished_cur1 = 1, finished_cur2 = 1;

DELETE FROM temp_table;
ALTER TABLE temp_table auto_increment = 1;

SET @sql = CONCAT('INSERT INTO temp_table(',
' SELECT NULL, DATE, PLANT_ID, ', ph_title, ', ', ph_abnd ,' FROM ', fg_tbl, ' WHERE YEAR(DATE) IN (', syear, 
') AND PLANT_ID IN (SELECT PLANT_ID FROM focal_plant_info WHERE SITE_CODE IN (',st_code,') AND SPP_CODE IN (',
spc_code_lst,')))'
);

-- SELECT @sql;
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

DROP TEMPORARY TABLE IF EXISTS eventTMP;

CREATE TEMPORARY TABLE IF NOT EXISTS eventTMP(
    seq1 INT NOT NULL,
	seq2 INT NOT NULL,
    plantId varchar(8) NOT NULL,
    date1 DATE,
	date2 DATE,
	ptitle1 tinyint,
    ptitle2 tinyint,
    diff tinyint,
	pintensity1 smallint, 
    pintensity2 smallint
) ENGINE=MEMORY;
 
INSERT INTO eventTMP(
SELECT tb1.seq,  tb2.seq, tb1.plantId, tb1.obsDate, tb2.obsDate,
tb1.phnTitle, tb2.phnTitle,
tb1.phnTitle-tb2.phnTitle, tb1.phnIntensity, tb2.phnIntensity
FROM temp_table AS tb1, temp_table AS tb2
WHERE tb1.seq+1 = tb2.seq AND year(tb1.obsDate) = year(tb2.obsDate) AND tb1.plantId = tb2.plantId);

DROP TEMPORARY TABLE IF EXISTS InTMP;
CREATE TEMPORARY TABLE IF NOT EXISTS InTMP
(PlantId varchar(8) NOT NULL, ObsDate Date, Intensity smallint);


OPEN cur1;
OPEN cur2;
emp_loop: LOOP
	FETCH NEXT FROM cur1 INTO pid, phsdate;
	FETCH NEXT FROM cur2 INTO pid, phedate;	
		IF finished_cur1 = 1 OR finished_cur2 = 1 THEN       /* No more rows*/
			LEAVE emp_loop;
		END IF;

	INSERT INTO InTMP (
    SELECT plantId, obsDate, phnIntensity FROM temp_table 
    WHERE plantId = pid AND obsDate BETWEEN phsdate AND phedate
	);
END LOOP emp_loop;
CLOSE cur1;
CLOSE cur2;

SELECT PlantId, DATE_FORMAT(ObsDate,'%m-%d-%Y'), Intensity FROM InTMP;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_get_eventcodes` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE PROCEDURE `sp_get_eventcodes`(IN fgcode varchar(2))
BEGIN
SELECT TITLE_NAME, TITLE_DISPLAY_NAME FROM pheno_title_info WHERE FUNC_GRP_CODE = fgcode AND 
(TITLE_CATEGORY IS NOT NULL AND LENGTH(TITLE_CATEGORY) != 0);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_get_eventresult` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE PROCEDURE `sp_get_eventresult`(IN fg_tbl varchar(10), IN spc_code_lst varchar(500),
 IN st_code varchar(500), IN ph_title varchar(6), IN syear varchar(500))
BEGIN
DELETE FROM temp_table;
ALTER TABLE temp_table auto_increment = 1;

SET @sql = CONCAT('INSERT INTO temp_table(',
' SELECT NULL, DATE, PLANT_ID, ', ph_title, ', NULL FROM ', fg_tbl, ' WHERE YEAR(DATE) IN (', syear, 
') AND PLANT_ID IN (SELECT PLANT_ID FROM focal_plant_info WHERE SITE_CODE IN (',st_code,') AND SPP_CODE IN (',
spc_code_lst,')))'
);

-- SELECT @sql;
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;


DROP TEMPORARY TABLE IF EXISTS eventTMP;

CREATE TEMPORARY TABLE IF NOT EXISTS eventTMP(
    seq1 INT NOT NULL,
	seq2 INT NOT NULL,
    plantId varchar(8) NOT NULL,
    date1 DATE,
	date2 DATE,
	ptitle1 tinyint,
    ptitle2 tinyint,
    diff tinyint
) ENGINE=MEMORY;
 
INSERT INTO eventTMP(
SELECT tb1.seq,  tb2.seq, tb1.plantId, tb1.obsDate, tb2.obsDate,
tb1.phnTitle, tb2.phnTitle,
tb1.phnTitle-tb2.phnTitle
FROM temp_table AS tb1, temp_table AS tb2
WHERE tb1.seq+1 = tb2.seq AND year(tb1.obsDate) = year(tb2.obsDate) AND tb1.plantId = tb2.plantId);

SELECT plantId, DATE_FORMAT(date1, '%m-%d-%Y'), DATE_FORMAT(date2, '%m-%d-%Y'), ptitle1, ptitle2, DATEDIFF(date2,date1) as days FROM eventTMP 
where diff = -1 
AND ptitle2 != 2; /*Added later to avoid dates when data was not recorded*/
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_get_funcgrpcodes` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE PROCEDURE `sp_get_funcgrpcodes`()
BEGIN
SELECT DISTINCT FUNC_GRP, FUNC_GRP_CODE FROM `species_info`; 
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_get_sitecodes` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE PROCEDURE `sp_get_sitecodes`()
BEGIN
SELECT SITE_CODE, SITE_NAME FROM site_info; 
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_get_speciescodes` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE PROCEDURE `sp_get_speciescodes`(IN fgcode varchar(2), IN stcode varchar(500))
BEGIN
SET @sql = CONCAT('SELECT DISTINCT SPP_CODE FROM focal_plant_info WHERE FUNC_GRP_CODE = \'', 
fgcode, '\' AND SITE_CODE  IN (', stcode, ')'
);

PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_get_statusresult` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE PROCEDURE `sp_get_statusresult`(IN fg_tbl varchar(10), IN spc_code_lst varchar(500),
 IN st_code varchar(500), IN ph_title varchar(6), IN syear varchar(500))
BEGIN
DECLARE pid1 varchar(8);
DECLARE pid2 varchar(8);
DECLARE phsdate date;
DECLARE phedate date;
DECLARE finished_cur1 tinyint DEFAULT 0;
DECLARE finished_cur2 tinyint DEFAULT 0;


DECLARE cur1 CURSOR FOR (SELECT plantId, date2 FROM eventTMP WHERE diff = -1
GROUP BY plantId, date2);

DECLARE cur2 CURSOR FOR (SELECT plantId, date2 FROM eventTMP WHERE diff = 1
GROUP BY plantId, date2);

-- DECLARE CONTINUE HANDLER FOR NOT FOUND SET finished_cur1 = 1;
-- DECLARE CONTINUE HANDLER FOR NOT FOUND SET finished_cur2 = 1;
DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET finished_cur1 = 1, finished_cur2 = 1;

DELETE FROM temp_table;
ALTER TABLE temp_table auto_increment = 1;

SET @sql = CONCAT('INSERT INTO temp_table(',
' SELECT NULL, DATE, PLANT_ID, ', ph_title, ', NULL FROM ' , fg_tbl, ' WHERE YEAR(DATE) IN (', syear, 
') AND PLANT_ID IN (SELECT PLANT_ID FROM focal_plant_info WHERE SITE_CODE IN (',st_code,') AND SPP_CODE IN (',
spc_code_lst,')))'
);

-- SELECT @sql;
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

DROP TEMPORARY TABLE IF EXISTS eventTMP;

CREATE TEMPORARY TABLE IF NOT EXISTS eventTMP(
    seq1 INT NOT NULL,
	seq2 INT NOT NULL,
    plantId varchar(8) NOT NULL,
    date1 DATE,
	date2 DATE,
	ptitle1 tinyint,
    ptitle2 tinyint,
    diff tinyint,
	pintensity1 smallint, 
    pintensity2 smallint
) ENGINE=MEMORY;
 
INSERT INTO eventTMP(
SELECT tb1.seq,  tb2.seq, tb1.plantId, tb1.obsDate, tb2.obsDate,
tb1.phnTitle, tb2.phnTitle,
tb1.phnTitle-tb2.phnTitle, tb1.phnIntensity, tb2.phnIntensity
FROM temp_table AS tb1, temp_table AS tb2
WHERE tb1.seq+1 = tb2.seq AND year(tb1.obsDate) = year(tb2.obsDate) AND tb1.plantId = tb2.plantId);

DROP TEMPORARY TABLE IF EXISTS InTMP;
CREATE TEMPORARY TABLE IF NOT EXISTS InTMP
(PlantId varchar(8) NOT NULL, StartDate Date, EndDate Date, Duration smallint);


OPEN cur1;
OPEN cur2;
FETCH NEXT FROM cur1 INTO pid1, phsdate;
FETCH NEXT FROM cur2 INTO pid2, phedate;
emp_loop: LOOP
		IF finished_cur1 = 1 OR finished_cur2 = 1 THEN       /* No more rows*/
			LEAVE emp_loop;
		END IF;
		IF (STRCMP(pid1 , pid2) = 0) AND phedate > phsdate THEN 
			set @datediff = DATEDIFF(phedate, phsdate); 
			INSERT INTO InTMP VALUES(pid1, phsdate, phedate, @datediff);
			FETCH NEXT FROM cur1 INTO pid1, phsdate;
			FETCH NEXT FROM cur2 INTO pid2, phedate;
		ELSEIF (STRCMP(pid1 , pid2) = 0) AND phedate < phsdate THEN
			FETCH NEXT FROM cur2 INTO pid2, phedate;
		ELSE
			FETCH NEXT FROM cur1 INTO pid1, phsdate;
		END IF;
END LOOP emp_loop;
CLOSE cur1;
CLOSE cur2;

SELECT PlantId, DATE_FORMAT(StartDate,'%m-%d-%Y'), DATE_FORMAT(EndDate,'%m-%d-%Y'),  Duration FROM InTMP;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_get_years` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE PROCEDURE `sp_get_years`(IN fgTbl varchar(10))
BEGIN
SET @sql = CONCAT('SELECT DISTINCT YEAR(DATE) AS YEAR FROM ', fgTbl);

PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_tst_get_statusresult` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE PROCEDURE `sp_tst_get_statusresult`(IN fg_tbl varchar(10), IN spc_code_lst varchar(500),
 IN st_code varchar(500), IN ph_title varchar(6), IN syear varchar(500))
BEGIN
DELETE FROM temp_table;
ALTER TABLE temp_table auto_increment = 1;

SET @sql = CONCAT('INSERT INTO temp_table(',
' SELECT NULL, DATE, PLANT_ID, ', ph_title,', NULL FROM ', fg_tbl, ' WHERE YEAR(DATE) IN (',syear,
') AND PLANT_ID IN (SELECT PLANT_ID FROM focal_plant_info WHERE SITE_CODE IN (' ,st_code,
') AND SPP_CODE IN (',spc_code_lst,')))'
);

-- SELECT @sql;
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

DROP TEMPORARY TABLE IF EXISTS eventTMP;

CREATE TEMPORARY TABLE IF NOT EXISTS eventTMP(
    seq1 INT NOT NULL,
	seq2 INT NOT NULL,
    plantId varchar(8) NOT NULL,
    date1 DATE,
	date2 DATE,
	ptitle1 tinyint,
    ptitle2 tinyint,
    diff tinyint
) ENGINE=MEMORY;
 
INSERT INTO eventTMP(
SELECT tb1.seq,  tb2.seq, tb1.plantId, tb1.obsDate, tb2.obsDate,
tb1.phnTitle, tb2.phnTitle,
tb1.phnTitle-tb2.phnTitle
FROM temp_table AS tb1, temp_table AS tb2
WHERE tb1.seq+1 = tb2.seq AND year(tb1.obsDate) = year(tb2.obsDate) AND tb1.plantId = tb2.plantId);


select plantId, DATE_FORMAT(MIN(date1),'%m-%d-%Y'), DATE_FORMAT(MAX(date2),'%m-%d-%Y'), DATEDIFF(MAX(date2),MIN(date1)) AS Duration from eventTMP 
WHERE (ptitle1 = 1 AND ptitle2 = 1) AND year(date1) = year(date2)
group by plantId, year(date1);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Final view structure for view `vw_ds`
--

/*!50001 DROP TABLE IF EXISTS `vw_ds`*/;
/*!50001 DROP VIEW IF EXISTS `vw_ds`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50001 VIEW `vw_ds` AS select `dd`.`DATE` AS `Date`,`dd`.`DAY_OF_YEAR` AS `JulianDay`,`fp`.`SITE_CODE` AS `Site`,`ds`.`PLANT_ID` AS `PlantID`,`fp`.`SPP_CODE` AS `SpeciesCode`,`fp`.`FUNC_GRP_CODE` AS `FuncGrpCode`,`sv`.`OBSERVER_ID` AS `ObserverID`,`sn`.`SITE_NOTES` AS `SiteNotes`,`ds`.`DS_01` AS `DS_01`,`ds`.`DS_02` AS `DS_02`,`ds`.`DS_202` AS `DS_202`,`ds`.`DS_03` AS `DS_03`,`ds`.`DS_04` AS `DS_04`,`ds`.`DS_05` AS `DS_05`,`ds`.`DS_06` AS `DS_06`,`ds`.`DS_213` AS `DS_213`,`ds`.`DS_214` AS `DS_214`,`ds`.`DS_07` AS `DS_07`,`ds`.`DS_207` AS `DS_207`,`ds`.`DS_08` AS `DS_08`,`ds`.`DS_208` AS `DS_208`,`ds`.`DS_09` AS `DS_09`,`ds`.`DS_209` AS `DS_209`,`ds`.`DS_10` AS `DS_10`,`ds`.`DS_210` AS `DS_210`,`ds`.`DS_11` AS `DS_11`,`ds`.`DS_211` AS `DS_211`,`ds`.`DS_12` AS `DS_12`,`pn`.`PLANT_NOTES` AS `PlantNotes`,`ph`.`PHOTO_NAME` AS `PhotoName` from ((((((`date_doy` `dd` join `ds_pheno` `ds` on((`dd`.`DATE` = `ds`.`DATE`))) join `focal_plant_info` `fp` on((`ds`.`PLANT_ID` = `fp`.`PLANT_ID`))) join `site_visit` `sv` on(((`sv`.`DATE` = `ds`.`DATE`) and (`fp`.`SITE_CODE` = `sv`.`SITE_CODE`)))) left join `site_note` `sn` on(((`sn`.`DATE` = `ds`.`DATE`) and (`fp`.`SITE_CODE` = `sn`.`SITE_CODE`)))) left join `plant_note` `pn` on(((`ds`.`PLANT_ID` = `pn`.`PLANT_ID`) and (`ds`.`DATE` = `pn`.`DATE`)))) left join `photo_info` `ph` on(((`ds`.`PLANT_ID` = `ph`.`PLANT_ID`) and (`ds`.`DATE` = `ph`.`DATE`)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_es`
--

/*!50001 DROP TABLE IF EXISTS `vw_es`*/;
/*!50001 DROP VIEW IF EXISTS `vw_es`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50001 VIEW `vw_es` AS select `dd`.`DATE` AS `Date`,`dd`.`DAY_OF_YEAR` AS `JulianDay`,`fp`.`SITE_CODE` AS `Site`,`es`.`PLANT_ID` AS `PlantID`,`fp`.`SPP_CODE` AS `SpeciesCode`,`fp`.`FUNC_GRP_CODE` AS `FuncGrpCode`,`sv`.`OBSERVER_ID` AS `ObserverID`,`sn`.`SITE_NOTES` AS `SiteNotes`,`es`.`BE_01` AS `BE_01`,`es`.`BE_02` AS `BE_02`,`es`.`BE_03` AS `BE_03`,`es`.`BE_203` AS `BE_203`,`es`.`BE_04` AS `BE_04`,`es`.`BE_204` AS `BE_204`,`es`.`BE_05` AS `BE_05`,`es`.`BE_05` AS `BE_205`,`es`.`BE_06` AS `BE_06`,`es`.`BE_206` AS `BE_206`,`es`.`BE_07` AS `BE_07`,`es`.`BE_207` AS `BE_207`,`pn`.`PLANT_NOTES` AS `PlantNotes`,`ph`.`PHOTO_NAME` AS `PhotoName` from ((((((`date_doy` `dd` join `es_pheno` `es` on((`dd`.`DATE` = `es`.`DATE`))) join `focal_plant_info` `fp` on((`es`.`PLANT_ID` = `fp`.`PLANT_ID`))) join `site_visit` `sv` on(((`sv`.`DATE` = `es`.`DATE`) and (`fp`.`SITE_CODE` = `sv`.`SITE_CODE`)))) left join `site_note` `sn` on(((`sn`.`DATE` = `es`.`DATE`) and (`fp`.`SITE_CODE` = `sn`.`SITE_CODE`)))) left join `plant_note` `pn` on(((`es`.`PLANT_ID` = `pn`.`PLANT_ID`) and (`es`.`DATE` = `pn`.`DATE`)))) left join `photo_info` `ph` on(((`es`.`PLANT_ID` = `ph`.`PLANT_ID`) and (`es`.`DATE` = `ph`.`DATE`)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_pg`
--

/*!50001 DROP TABLE IF EXISTS `vw_pg`*/;
/*!50001 DROP VIEW IF EXISTS `vw_pg`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50001 VIEW `vw_pg` AS select `dd`.`DATE` AS `Date`,`dd`.`DAY_OF_YEAR` AS `JulianDay`,`fp`.`SITE_CODE` AS `Site`,`pg`.`PLANT_ID` AS `PlantID`,`fp`.`SPP_CODE` AS `SpeciesCode`,`fp`.`FUNC_GRP_CODE` AS `FuncGrpCode`,`sv`.`OBSERVER_ID` AS `ObserverID`,`sn`.`SITE_NOTES` AS `SiteNotes`,`pg`.`GR_01` AS `GR_01`,`pg`.`GR_02` AS `GR_02`,`pg`.`GR_202` AS `GR_202`,`pg`.`GR_03` AS `GR_03`,`pg`.`GR_04` AS `GR_04`,`pg`.`GR_204` AS `GR_204`,`pg`.`GR_05` AS `GR_05`,`pg`.`GR_205` AS `GR_205`,`pg`.`GR_06` AS `GR_06`,`pg`.`GR_206` AS `GR_206`,`pg`.`GR_07` AS `GR_07`,`pg`.`GR_207` AS `GR_207`,`pg`.`GR_08` AS `GR_08`,`pg`.`GR_09` AS `GR_09`,`pg`.`GR_209` AS `GR_209`,`pn`.`PLANT_NOTES` AS `PlantNotes`,`ph`.`PHOTO_NAME` AS `PhotoName` from ((((((`date_doy` `dd` join `pg_pheno` `pg` on((`dd`.`DATE` = `pg`.`DATE`))) join `focal_plant_info` `fp` on((`pg`.`PLANT_ID` = `fp`.`PLANT_ID`))) join `site_visit` `sv` on(((`sv`.`DATE` = `pg`.`DATE`) and (`fp`.`SITE_CODE` = `sv`.`SITE_CODE`)))) left join `site_note` `sn` on(((`sn`.`DATE` = `pg`.`DATE`) and (`fp`.`SITE_CODE` = `sn`.`SITE_CODE`)))) left join `plant_note` `pn` on(((`pg`.`PLANT_ID` = `pn`.`PLANT_ID`) and (`pg`.`DATE` = `pn`.`DATE`)))) left join `photo_info` `ph` on(((`pg`.`PLANT_ID` = `ph`.`PLANT_ID`) and (`pg`.`DATE` = `ph`.`DATE`)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_su`
--

/*!50001 DROP TABLE IF EXISTS `vw_su`*/;
/*!50001 DROP VIEW IF EXISTS `vw_su`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50001 VIEW `vw_su` AS select `dd`.`DATE` AS `Date`,`dd`.`DAY_OF_YEAR` AS `JulianDay`,`fp`.`SITE_CODE` AS `Site`,`su`.`PLANT_ID` AS `PlantID`,`fp`.`SPP_CODE` AS `SpeciesCode`,`fp`.`FUNC_GRP_CODE` AS `FuncGrpCode`,`sv`.`OBSERVER_ID` AS `ObserverID`,`sn`.`SITE_NOTES` AS `SiteNotes`,`su`.`CA_01` AS `CA_01`,`su`.`CA_201` AS `CA_201`,`su`.`CA_02` AS `CA_02`,`su`.`CA_202` AS `CA_202`,`su`.`CA_03` AS `CA_03`,`su`.`CA_203` AS `CA_203`,`su`.`CA_04` AS `CA_04`,`su`.`CA_204` AS `CA_204`,`su`.`CA_05` AS `CA_05`,`su`.`CA_205` AS `CA_205`,`pn`.`PLANT_NOTES` AS `PlantNotes`,`ph`.`PHOTO_NAME` AS `PhotoName` from ((((((`date_doy` `dd` join `su_pheno` `su` on((`dd`.`DATE` = `su`.`DATE`))) join `focal_plant_info` `fp` on((`su`.`PLANT_ID` = `fp`.`PLANT_ID`))) join `site_visit` `sv` on(((`sv`.`DATE` = `su`.`DATE`) and (`fp`.`SITE_CODE` = `sv`.`SITE_CODE`)))) left join `site_note` `sn` on(((`sn`.`DATE` = `su`.`DATE`) and (`fp`.`SITE_CODE` = `sn`.`SITE_CODE`)))) left join `plant_note` `pn` on(((`su`.`PLANT_ID` = `pn`.`PLANT_ID`) and (`su`.`DATE` = `pn`.`DATE`)))) left join `photo_info` `ph` on(((`su`.`PLANT_ID` = `ph`.`PLANT_ID`) and (`su`.`DATE` = `ph`.`DATE`)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-12-08 10:19:02
