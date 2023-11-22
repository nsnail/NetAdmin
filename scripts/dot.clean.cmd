call npm --prefix ../src/frontend/admin run prettier
dot rbom -w -e refs -e .git -e node_modules ../
dot trim -w -e refs -e .git -e node_modules ../
dot tolf -w -e refs -e .git -e node_modules ../