``` 
//存储、工作单元、事务使用
    class MyClass
    {
        static void Main(string[] args)
        {
            using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
            {
                //事务范围
                ts.Complete();//全部提交
            }
        }
    }
    namespace ABC
    {
        class MyClass
        {
            private readonly IRepository<Product, Guid> _productRepository;
            private readonly IRepository<Project, int> _projectrepository;
            public MyClass(IRepository<Product, Guid> productrepository, IRepository<Project, int> projectrepository)
            {
                _productRepository = productrepository;
                _projectrepository = projectrepository;
            }
            public void test()
            {

                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.RequiresNew))
                {
                    _productRepository.InsertAsync(new Product() { }, true);
                    _productRepository.InsertAsync(new Product() { });
                    _productRepository.InsertAsync(new Product() { });
                    _productRepository.UnitOfWork.SaveChangesAsync();

                    _projectrepository.UpdateAsync(new Project() { });
                    _projectrepository.UpdateAsync(new Project() { });
                    _projectrepository.UnitOfWork.SaveChangesAsync();
                    //事务范围
                    ts.Complete();//全部提交
                }
            }
        }
    }
```

```
//一个线程租户切换
//namespace ABC
//{
//    class MyClass
//    {
//        static void test(string[] args)
//        {
//            QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate.ICurrentTenant currentTenant = null!;
//            using (currentTenant.Change(Guid.NewGuid()))
//            {
//                currentTenant.Id //002
//            }
//            currentTenant.Id//001
//        }
//    }
//}
```