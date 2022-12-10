require("base")
child2 = base:new()
function child2:new(c, d)
    o = base:new(c)
    setmetatable(o, self)
    self.__index = self;
    self.a = c * d;
    return o;
end

function child2:funcA()
    print('child2' .. self.a);
end
