#!/bin/bash

# Ensure a problem number is provided
if [ -z "$1" ]; then
  echo "Usage: $0 <problem_number>"
  exit 1
fi

# Format problem number to 4 digits
problem_number=$(printf "%04d" $1)

# Determine the range folder
range_folder=$(printf "%04d" $(( (10#$problem_number / 100) * 100 )))

# Check if Template folder exists
if [ ! -d "Template" ]; then
  echo "Template folder not found!"
  exit 1
fi

# Create the range folder if it doesn't exist
mkdir -p "$range_folder"

# Copy Template to the range folder and rename it
cp -r "Template" "$range_folder/LeetCode.Problem$problem_number"

# Go to the newly created problem folder
cd "$range_folder/LeetCode.Problem$problem_number" || exit

# Rename the `.csproj` file
template_csproj="LeetCode.ProblemNNNN.csproj"
new_csproj="LeetCode.Problem$problem_number.csproj"

if [ -f "$template_csproj" ]; then
  mv "$template_csproj" "$new_csproj"
else
  echo "$template_csproj not found!"
  exit 1
fi
