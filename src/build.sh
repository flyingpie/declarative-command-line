#!/usr/bin/env bash

set -eo pipefail

OUTPUT=artifacts/package
PRERELEASE=${PRERELEASE:-TRUE}
PRERELEASE=${PRERELEASE^^}
CONF=${CONF:-release}
VERSIONSUFFIX=""
if [[ "$PRERELEASE" != "FALSE" ]]; then
	VERSIONSUFFIX="$(git rev-parse --short=8 HEAD)"
fi

echo "#######################################"
echo "# CONF:.............$CONF"
echo "# OUTPUT:...........$OUTPUT"
echo "# PRERELEASE:.......$PRERELEASE"
echo "# VERSIONSUFFIX:....$VERSIONSUFFIX"
echo "#######################################"

rm -rf "$OUTPUT"
mkdir -p "$OUTPUT"

dotnet test "./src/DeclarativeCommandLine.UnitTest" --configuration $CONF

dotnet pack "./src/DeclarativeCommandLine" \
	--configuration $CONF \
	--output "$OUTPUT" \
	/p:VersionSuffix="$VERSIONSUFFIX"

dotnet pack "./src/DeclarativeCommandLine.Generator" \
	--configuration $CONF \
	--output "$OUTPUT" \
	/p:VersionSuffix="$VERSIONSUFFIX"

dotnet nuget push "$OUTPUT/*" --api-key pat --source nuget.org
