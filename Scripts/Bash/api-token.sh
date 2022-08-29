#!/bin/bash
TOKEN="$(curl https://localhost:8443/auth/login -d '{"username":"admin","password":"P@ssw0rd"}' -H 'Content-Type: application/json' -s | jq -r .token)"

echo "$TOKEN"
