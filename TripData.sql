CREATE DATABASE `tripdata` /*!40100 DEFAULT CHARACTER SET latin1 */;

CREATE TABLE `iotcameralogs_tb` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceid` varchar(45) DEFAULT NULL,
  `anpr` varchar(45) DEFAULT NULL,
  `capturedt` datetime DEFAULT NULL,
  `entrydt` datetime DEFAULT NULL,
  `longtitude` varchar(45) DEFAULT NULL,
  `latitude` varchar(45) DEFAULT NULL,
  `siteid` varchar(45) DEFAULT NULL,
  `stationid` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
CREATE TABLE `mobiledevice_tb` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `imei` varchar(100) DEFAULT NULL,
  `phoneno` varchar(45) DEFAULT NULL,
  `entrydt` datetime DEFAULT NULL,
  `isenable` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
CREATE TABLE `mobiledevicelog_tb` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `imei` varchar(100) DEFAULT NULL,
  `phoneno` varchar(45) DEFAULT NULL,
  `entrydt` datetime DEFAULT NULL,
  `longitude` varchar(45) DEFAULT NULL,
  `latitude` varchar(45) DEFAULT NULL,
  `tripid` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
CREATE TABLE `states_tb` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `statename` varchar(45) DEFAULT NULL,
  `entrydt` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=64 DEFAULT CHARSET=latin1;
CREATE TABLE `transportevent_tb` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tranportevent` varchar(45) DEFAULT NULL,
  `entrydt` datetime DEFAULT NULL,
  `isenable` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;
CREATE TABLE `transporteventlog_tb` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `eventid` int(11) DEFAULT NULL,
  `entrydt` datetime DEFAULT NULL,
  `imei` varchar(100) DEFAULT NULL,
  `phoneno` varchar(45) DEFAULT NULL,
  `longtitude` varchar(45) DEFAULT NULL,
  `latitude` varchar(45) DEFAULT NULL,
  `tripid` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
CREATE TABLE `transportmode_tb` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `transportmode` varchar(45) DEFAULT NULL,
  `entrydt` datetime DEFAULT NULL,
  `isenable` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
CREATE TABLE `transportstations_tb` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `stationname` varchar(45) DEFAULT NULL,
  `transportmodeid` int(11) DEFAULT NULL,
  `stateid` int(11) DEFAULT NULL,
  `operatorid` varchar(45) DEFAULT NULL,
  `entrydt` datetime DEFAULT NULL,
  `isenable` int(11) DEFAULT NULL,
  `stationqrcodeid` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;
CREATE TABLE `transportstationsoperator_tb` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `operatorid` varchar(45) DEFAULT NULL,
  `operatorname` varchar(45) DEFAULT NULL,
  `entrydt` datetime DEFAULT NULL,
  `isenable` int(11) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
CREATE TABLE `triplogs_tb` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tripid` int(11) DEFAULT NULL,
  `imei` varchar(45) DEFAULT NULL,
  `phoneno` varchar(45) DEFAULT NULL,
  `entrydt` datetime DEFAULT NULL,
  `startstop` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
CREATE TABLE `tripmaps_tb` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `startstationid` int(11) DEFAULT NULL,
  `endstationid` int(11) DEFAULT NULL,
  `entrydt` datetime DEFAULT NULL,
  `isenable` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
 