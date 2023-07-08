{ lib, fetchFromGitHub, buildDotnetModule, dotnet-runtime }:
let
  pname = "battlenet-lancache-prefill";
  version = "1.6.1";

  meta = with lib; {
    homepage = "https://github.com/tpill90/battlenet-lancache-prefill";
    description =
      "CLI tool to automatically prefill a Lancache with Battle.Net games";
    license = licenses.mit;
    inherit (dotnet-runtime.meta) platforms;
  };

  src = fetchFromGitHub {
    owner = "JayRovacsek";
    repo = "battlenet-lancache-prefill";
    rev = "592fcefe80df3fa022d3f680b5ab1eef67130094";
    fetchSubmodules = true;
    hash = "sha256-DDxvjMtFcbXCZUxz1fYRH3D7gp9bzu3TvySpTcG/gZU=";
  };

in buildDotnetModule {
  inherit pname version meta src dotnet-runtime;

  projectFile = [
    "BattleNetPrefill/BattleNetPrefill.csproj"
    "LancachePrefill.Common/LancachePrefill.Common.csproj"
  ];

  nugetDeps = ./deps.nix;
}
