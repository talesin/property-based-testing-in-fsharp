#!/usr/bin/env bash
DIR=`cd $(dirname "$0")/..; pwd`
pushd $DIR/src/Eg > /dev/null
dotnet test $@
popd > /dev/null