#!/bin/bash

CURRENT_DIR="$(pwd)"
python3 ../wasm_config.py "$CURRENT_DIR" "$1"
