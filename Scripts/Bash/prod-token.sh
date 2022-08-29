#!/bin/bash
TOKEN="$(curl https://vuln.maneea.net/auth/login -k -d '{"username":"admin","password":"P@ssw0rd"}' -H 'Content-Type: application/json' | jq -r .token)"

echo "$TOKEN"
