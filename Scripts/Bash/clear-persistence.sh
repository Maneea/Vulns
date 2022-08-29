#!/bin/bash

CURL_TO_ELASTIC='curl -u elastic:Ao=Ec8eTVg0Y7W+X-snr -ks https://localhost:9200'
INDICES=$($CURL_TO_ELASTIC/_aliases | jq -r 'keys[]' | grep -Ev '^\.')
__DIR="$(printf "$(pwd)/$0" | sed -e 's/\/\.\//\//g' | grep -Eo '^.+/')"

for INDEX in $INDICES
do
    echo DELETING INDEX $INDEX . . .
    $CURL_TO_ELASTIC/$INDEX -XDELETE
    echo # Line-Feed :)
done

cd $__DIR../../

dotnet ef database drop     -s Web -p Infrastructure -f
dotnet ef database update   -s Web -p Infrastructure

# At this point, all indices in ES and tables in SQL were removed and regenerated
