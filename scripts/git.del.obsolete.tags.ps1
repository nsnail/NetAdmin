git push origin :refs/tags/$(git tag -l "*-*")
git tag -d $(git tag -l "*-*")