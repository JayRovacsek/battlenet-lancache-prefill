{
  description = "A very basic flake";

  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs/nixpkgs-unstable";
    flake-utils.url = "github:numtide/flake-utils";
    flake-compat = {
      url = "github:edolstra/flake-compat";
      flake = false;
    };
  };

  outputs = { self, nixpkgs, flake-utils, ... }:
    flake-utils.lib.eachDefaultSystem (system:
      let pkgs = nixpkgs.legacyPackages.${system};
      in {
        devShells.default = pkgs.mkShell {
          name = "dotnet-dev-shell";
          packages =
            self.packages.x86_64-linux.battlenet-lancache-prefill.nativeBuildInputs
            ++ (with pkgs; [ nixfmt ]);
        };
        packages = {
          battlenet-lancache-prefill =
            nixpkgs.legacyPackages.x86_64-linux.callPackage ./default.nix { };
          default = self.packages.x86_64-linux.battlenet-lancache-prefill;
        };
      });
}
