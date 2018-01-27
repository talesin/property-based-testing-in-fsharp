#!/usr/bin/env bash
DIR=`cd $(dirname "$0")/..; pwd`
pushd $DIR
dotnet build --packages $DIR/packages
popd