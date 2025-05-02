#!/bin/bash

dotnet ef migrations add $1 \
  --project ../Personal.Data \
  --startup-project . \
  --output-dir Migrations
