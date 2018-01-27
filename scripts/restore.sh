#!/usr/bin/env bash
DIR=`cd $(dirname "$0")/..; pwd`
pushd $DIR
dotnet restore --packages $DIR/packages
popd $DIR