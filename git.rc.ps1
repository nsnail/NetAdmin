$branch = $( git branch --show-current )
git checkout main
git pull
git branch -D $branch
git branch $branch
git checkout $branch