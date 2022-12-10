require("base")
-- -----------------------------------
testBase = base:new(10);
testBase:funcA();
-------------------------------------
require("child")
-- ---------------------------------------
testChild = child:new(10);
testChild:funcA();
-- -------------------------------------
require("child2")
-- ---------------------------------------
testChild2 = child2:new(10,20);
testChild2:funcA();
-- -------------------------------------

