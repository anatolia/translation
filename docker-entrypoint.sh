#!/bin/bash

echo "Changing Translation.Client.Web.dll.config file"
sed -i -e "s/_DBUSER_/"$DBUSER"/" \
       -e "s/_DBPASS_/"$DBPASS"/" \
       -e "s/_DBCONN_/"$DBCONN"/" \
       -e "s/_DBNAME_/"$DBNAME"/" \
       -e "s/_DBPORT_/"$DBPORT"/" \
       -e "s/_SUPERP_/"$SUPERP"/" Translation.Client.Web.dll.config

echo "Starting dotnet Translation.Client.Web.dll"
dotnet Translation.Client.Web.dll
