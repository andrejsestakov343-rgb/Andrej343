using Domain.Departments;
using Domain.Departments.ValueObjects;
using Domain.LocationContext.ValueObjects;

namespace TestProject1
{    
        public class LocationNameTests
        {
            [Theory]
            [InlineData("Конференц-зал ")]
            [InlineData("Офис 1")]
            [InlineData("Главный зал")]
            [InlineData("Переговорный зал")]
            [InlineData("Ф")]
            public void Create_LocationName(string value)
            {
                var locationName = LocationName.Create(value);

                Assert.NotNull(locationName);
                Assert.Equal(value, locationName.Value);
            }

            [Theory]
            [InlineData("")]
            [InlineData("   ")]
            [InlineData(null)]
            [InlineData("Ф")]
            public void Create(string? value)
            {
                Assert.Throws<ArgumentException>(() => LocationName.Create(value!));
            }
        }

        public class AddressTests
        {

            [Theory]
            [InlineData("")]
            [InlineData("   ")]
            [InlineData(null)]
            public void Create(string? value)
            {
                Assert.Throws<ArgumentException>(() => LocationName.Create(value!));
            }
        }

        public class TimeZoneTests
        {
            [Theory]
            [InlineData("UTC")]
            [InlineData("UTC+1")]
            [InlineData("UTC+3")]
            [InlineData("UTC+4")]
            [InlineData("MSK")]
            [InlineData("KST")]
            [InlineData("EST")]
            public void Create_TimeZone(string value)
            {
                var timeZone = LocationTimeZone.Create(value);
                Assert.NotNull(timeZone);

            }

            [Theory]
            [InlineData("")]
            [InlineData("   ")]
            [InlineData(null)]
            [InlineData("XXX")]
            public void Create(string? value)
            {
                Assert.Throws<ArgumentException>(() => LocationTimeZone.Create(value!));

            }
        }

    public class DepartmentTests
    {
        [Fact]
        public void Create_Department()
        {
            var name =  DepartmentName.Create("IT Департамент");
            var identifier = DepartmentIdentifier.Create("it-dept");
            var department = Department.CreateRoot(name, identifier);

            
            Assert.NotNull(department);
            Assert.Equal("IT Департамент", department.Name.Value);
            Assert.Equal("it-dept", department.Identifier.Value);
            Assert.False(department.IsActive); 
            Assert.Empty(department.DepartmentPositions);
        }
        [Fact]
        public void Create_Department_Parent()
        {
            
            var parentName = DepartmentName.Create("Команда разработки");
            var parentIdentifier = DepartmentIdentifier.Create("dev-team");
            var childName = DepartmentName.Create("Группа c#");
            var childIdentifier = DepartmentIdentifier.Create("csharp-grp");

            var parent = Department.CreateRoot(parentName, parentIdentifier);
            var child = Department.CreateChild(childName, childIdentifier, parent);

            Assert.NotNull(parent);
            Assert.NotNull(child);
            Assert.Equal(parent.Id, child.ParentId);

        }
        [Fact]
        public void AddChild_Departent_Success()
        {
            var parent = Department.CreateRoot(DepartmentName.Create("Parent Department"), DepartmentIdentifier.Create("parent dept"));
            var child = Department.CreateRoot(DepartmentName.Create("Child Department"), DepartmentIdentifier.Create("child dept"));


            parent.AddChild(child);

            Assert.Equal(parent.Id, child.ParentId);
            
        }
        [Fact]
      
        public void ActivateDepartment_Success()
        {
            var deprtment = Department.CreateRoot(DepartmentName.Create("Department"), DepartmentIdentifier.Create("dept"));

            Assert.False(deprtment.IsActive);
            
            Assert.True(deprtment.IsActive);

        }
        [Fact]
        public void ArchiveDepartment_Success()
        {
            var parent = Department.CreateRoot(DepartmentName.Create("Parent"), DepartmentIdentifier.Create("parent"));
            var child = Department.CreateRoot(DepartmentName.Create("Child"), DepartmentIdentifier.Create("child"));
  

            Assert.False(parent.IsActive);
        }
        [Fact]
       
        
        public void AddChildDepartment()
        {
           
            var parent = Department.CreateRoot(DepartmentName.Create("Parent Department"), DepartmentIdentifier.Create("parent"));
            var child = Department.CreateChild(DepartmentName.Create("Child Department"), DepartmentIdentifier.Create("child"), parent);

            Assert.Throws<InvalidOperationException>(() => parent.AddChild(child));
        }
        [Fact]
        public void CreateDepartment()
        {
            var Name = DepartmentName.Create("IT department");
            var Identifier = DepartmentIdentifier.Create("it-dept");
            Department.CreateRoot(Name, Identifier);

            Assert.Throws<ArgumentException>(() => DepartmentName.Create(""));
        }
        [Fact]
        public void CreateDepartment2()
        {
            var Name1 = DepartmentName.Create("Department 1");
            var Identifier = DepartmentIdentifier.Create("dept-1");
            Department.CreateRoot(Name1, Identifier);
            var Name2 = DepartmentName.Create("Department 2");
            var Identifier2= DepartmentIdentifier.Create("dept-2");
            Department.CreateRoot(Name2, Identifier);

            Assert.Throws<ArgumentException>(() => DepartmentIdentifier.Create(""));
        }
        [Fact]
        public void CreateDeaprtment3()
        {
        Assert.Throws<ArgumentException>(() => DepartmentName.Create(" "));
        Assert.Throws<ArgumentException>(() => DepartmentName.Create(new string('a', 201)));
        } 
        [Fact]
        public void CreateDepartment4()
        {
            Assert.Throws<ArgumentException>(() => DepartmentIdentifier.Create(""));
            Assert.Throws<ArgumentException>(() => DepartmentIdentifier.Create("INVALID IDENTIFIER WITH SPACES"));
        }
        [Fact]
        public void CreateChildDepartment_Success()
        {
            var parent = Department.CreateRoot(DepartmentName.Create("Parent"), DepartmentIdentifier.Create("parent"));
            var child = Department.CreateRoot(DepartmentName.Create("Child"), DepartmentIdentifier.Create("child"));

            Assert.NotEqual(parent.Id, child.Id);
            Assert.NotNull(child);
            Assert.False(child.IsActive);
        }
        [Fact]
        public void Create_Success()
        {
            var level1 = Department.CreateRoot(DepartmentName.Create("Level 1"), DepartmentIdentifier.Create("level-1"));
            var level2 = Department.CreateChild(DepartmentName.Create("Level 2"), DepartmentIdentifier.Create("level-2"), level1);
            var level3 = Department.CreateChild(DepartmentName.Create("Level 3"), DepartmentIdentifier.Create("level-3"), level2);

            Assert.Equal(level1.Id, level2.ParentId);
            Assert.Equal(level2.Id, level3.ParentId);
        }
        [Fact]
        public void CreateDepartment5()
        {
            var department1 = Department.CreateRoot(DepartmentName.Create("Dept 1"), DepartmentIdentifier.Create("d1"));
            var department2 = Department.CreateChild(DepartmentName.Create("Dept 2"),DepartmentIdentifier.Create("d2"), department1);

            department1.AddChild(department2);
            Assert.Throws<InvalidOperationException>(() => department2.AddChild(department1));
        }
        public class DepartmentPosition
        {
            public Guid Id { get; private set; }
            public string PositionName { get; private set; }
            public int Rank {  get; private set; }

            public DepartmentPosition (string  positionName, Guid id, int rank)
            {
                if (string.IsNullOrEmpty(positionName))
                    throw new ArgumentException("Название должности не может быть пустым.", nameof(positionName));

                if (rank <= 0)
                    throw new ArgumentException("Ранг должен быть положительным числом.", nameof(rank));

                Id = Guid.NewGuid();
                PositionName = positionName;
                Rank = rank;
            }
            public void NewRank(int rank)
            {
                if (rank <= 0)
                    throw new ArgumentException("Ранг должен быть положительным числом.", nameof(rank));

                Rank = rank;
            }
        }
        public class Organization
        {
            public Guid Id { get; private set; }
            public string Name { get; private set; }
            public string Identifier { get; private set; }
            public Guid? ParentId { get; private set; }
            public bool IsActive { get; private set; }

            private readonly List<Organization> _children;
            private readonly List<DepartmentPosition> _positions;

            public Organization (string name, string identifier, Guid? parentId = null)
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("Название подразделения не может быть пустым", nameof(name));

                if (string.IsNullOrWhiteSpace(identifier))
                    throw new ArgumentException("Идентификатор подразделения не может быть пустым", nameof(identifier));

                Id = Guid.NewGuid();
                Name = name;
                Identifier = identifier;
                ParentId = parentId;
                IsActive = true;
                _children = new List<Organization> ();
                _positions = new List<DepartmentPosition>();
            } 
            public static Organization CreateRoot (string name, string identifier)
            {
                return new Organization (name, identifier);
            }
            public Organization CreateChild (string name, string identifier)
            {
                if (!IsActive)
                    throw new InvalidOperationException("Нельзя добавить должность в неактивное подразделение");

                var child = new Organization (name, identifier);
                _children.Add (child);
                return child;
            }
            public void AddChild (Organization child)
            {
              
                if (!IsActive)
                    throw new InvalidOperationException("Нельзя добавить дочернее подразделение в неактивное подразделение");

                if (child == null)
                    throw new InvalidOperationException("Подразделение уже имеет родительское подразделение");

                if (_children.Count == 0)
                    throw new InvalidOperationException("Добавление создаст циклическую ссылку");

                _children.Add (child); 
            }
            public void Activate()
            {
                IsActive = true;
            }
            public void Archive()
            {
                IsActive = false;
            }
            public void AddPosition (string PositionName, int rank)
            {
                if (!IsActive)
                    throw new InvalidOperationException("Нельзя добавить должность в неактивное подразделение");

                if (_positions.Any(p => p.Rank == rank))
                {
                    throw new InvalidOperationException($"Ранг {rank} уже занят в этом подразделении.");
                }
                var position = new DepartmentPosition(PositionName, Guid.NewGuid(), rank);
                _positions.Add(position);
            }
            public void ChangePositionRank(Guid positionId, int newRank)
            {
                if (!IsActive)
                    throw new InvalidOperationException("Нельзя изменить ранг в неактивном подразделении");

                var position = _positions.FirstOrDefault(p => p.Id == positionId);
                if (position == null)
                    throw new InvalidOperationException("Должность не найдена");

                if (_positions.Any(p => p.Rank == newRank && p.Id != positionId))
                    throw new InvalidOperationException($"Ранг {newRank} уже занят в этом подразделении.");

                position.NewRank(newRank);
            }
            public IReadOnlyList<Organization> GetChildren() => _children.AsReadOnly();
            public IReadOnlyList<DepartmentPosition> GetPositions() => _positions.AsReadOnly();
        }
        [Fact]
        public void AddPositionRank_Success()
        {
            var org = Organization.CreateRoot("Test", "t1");
            org.AddPosition("Directior", 1);

            var positons = org.GetPositions();
            Assert.Single(positons);
            Assert.Equal(1, positons[0].Rank);

            Assert.Throws<InvalidOperationException>(() =>org.AddPosition("Another", 1));
        }
        [Fact]
        public void ChangePositionRank_Success()
        {
            var org = Organization.CreateRoot("Test", "t2");
            org.AddPosition("Director", 1);
            org.AddPosition("Manager", 2);
            org.AddPosition("Developer", 3);

            var director = org.GetPositions()[0];
            var manager = org.GetPositions()[1];

            org.ChangePositionRank(director.Id, 3);

            Assert.Equal(3, org.GetPositions()[0].Rank);

  
            Assert.Throws<InvalidOperationException>(() => org.ChangePositionRank(manager.Id, 3));
        }
        [Fact]
        public void NewRank_Success()
        {
            var position = new DepartmentPosition("Test", Guid.NewGuid(), 1);

            position.NewRank(2);

            Assert.Equal(2, position.Rank); 

            Assert.Throws<ArgumentException>(() => position.NewRank(0));
            Assert.Throws<ArgumentException>(() => position.NewRank(-1));
        }
    }
}
