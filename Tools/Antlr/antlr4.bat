java -jar antlr-4.9.2-complete.jar -visitor -no-listener -Dlanguage=CSharp -o "../Min.RuleEngine" -package Min.RuleEngine "../../Min.RuleEngine\RuleEngine.g4"
@echo off
echo Completed!
pause