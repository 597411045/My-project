require("base")
child = base:new()
function child:new(c)
    o = base:new(c)
    setmetatable(o, self)
    self.__index = self;
    return o;
end

function child:funcA()
    print('child' .. self.a);
end