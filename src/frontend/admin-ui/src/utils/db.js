//初始数据库结构
const dbData = [
	{
		dbName: "masterDB",					//数据库名称
		version: 1,							//数据库版本号,当结构发生变化时
		tables: [							//表
			{
				name: "SYS_favorites",		//表名称
				keyPath: "uid",				//主键
				autoIncrement: false,		//主键是否自增
				index: [					//索引
					{
						name: "name_index",	//索引名称
						key: "name"			//索引key
					}
				]
			},
			{
				name: "SYS_keyword",
				keyPath: "id"
			}
		]
	},
	{
		dbName: "guestDB",
		version: 1,
		tables: [
			{
				name: "MY_demo",
				keyPath: "id"
			}
		]
	}
]

// 使用示例
// import DB from '@/utils/db'

// 初始化创建数据库 一般在项目启动时就执行了
// await DB.create()

// 打开某个数据库，返回数据库实例
// const database = await DB.open("dbName")

// 在打开的数据库中添加数据到tablenName表
// await database.add("tablenName", data)

// 查询
// await database.get("tablenName", key)

// 查询 根据索引
// await database.indexGet("tablenName", "indexName", indexVal)

// 修改
// await database.put("tablenName", data)

// 删除
// await database.delete("tablenName", key)

// 获取所有
// await database.getAll("tablenName")

// 清空某个表数据
// await database.clear("tablenName")

// 获取某个表信息
// database.getTable("tablenName")

// 获取所有表
// database.getTables()

// 关闭数据库连接
// database.close()


export default {
	//建立数据库，表，初始数据
	create() {
		var promiseArray = []
		const addDB = db => {
			return new Promise((resolve, reject) => {
				const request = indexedDB.open(db.dbName, db.version)
				request.onupgradeneeded = e => {
					const thisDB = e.target.result
					db.tables.forEach(item => {
						let table = null
						if (thisDB.objectStoreNames.contains(item.name)) {
							//已存在表，删除旧index
							table = e.target.transaction.objectStore(item.name)
							table.indexNames.length>0 && table.indexNames.forEach(indexName => {
								table.deleteIndex(indexName)
							})
						}else{
							//创建新的表
							table = thisDB.createObjectStore(item.name, {
							    keyPath: item.keyPath,
								autoIncrement: item.autoIncrement
							})
						}
						//建立index
						item.index && item.index.forEach(ind => {
							table.createIndex(ind.name, ind.key, { unique: false })
						})
					})
				},
				request.onsuccess = e => {
					return resolve(e.target.result)
				}
				request.onerror = e => {
					return reject(e)
				}
			})
		}
		dbData.forEach(db => {
			promiseArray.push(addDB(db))
		})
		return new Promise((resolve, reject) => {
			Promise.all(promiseArray).then((e) => {
				resolve(e)
			}).catch(e => {
				reject(e)
			})
		})
	},
	//所有数据库
	databases(){
		return indexedDB.databases()
	},
	//打开数据库
	open(dbName){
		return new Promise((resolve, reject) => {
			const request = indexedDB.open(dbName)
			request.onsuccess = e => {
				const database = new this.database(e.target.result)
				resolve(database)
			}
			request.onerror = e => {
				reject(e)
			}
		})
	},
	//删除数据库
	deleteDB(dbName){
		return indexedDB.deleteDatabase(dbName)
	},
	//数据库类
	database: function (IDBDatabase) {
		this.IDBDatabase = IDBDatabase

		/**
		 * 添加行数据
		 * @param {string} tableName 表名
		 * @param {object} data 数据
		 * @returns {promise}
		 */
		this.add = (tableName, data) => {
			return new Promise((resolve, reject) => {
				const request = IDBDatabase.transaction([tableName], 'readwrite').objectStore(tableName).add(data)
				request.onsuccess = e => {
					resolve(e)
				}
				request.onerror = e => {
					reject(e)
				}
			})
		}

		/**
		 * 修改行数据，未查询到就新增
		 * @param {string} tableName 表名
		 * @param {object} data 数据
		 * @returns {promise}
		 */
		this.put = (tableName, data) => {
			return new Promise((resolve, reject) => {
				const request = IDBDatabase.transaction([tableName], 'readwrite').objectStore(tableName).put(data)
				request.onsuccess = e => {
					resolve(e)
				}
				request.onerror = e => {
					reject(e)
				}
			})
		}

		/**
		 * 删除行
		 * @param {string} tableName 表名
		 * @param {string} key 主键
		 * @returns {promise}
		 */
		this.delete = (tableName, key) => {
			return new Promise((resolve, reject) => {
				const request = IDBDatabase.transaction([tableName], 'readwrite').objectStore(tableName).delete(key)
				request.onsuccess = e => {
					resolve(e)
				}
				request.onerror = e => {
					reject(e)
				}
			})
		}

		/**
		 * 根据主键获取行
		 * @param {string} tableName 表名
		 * @param {string} key 主键
		 * @returns {promise}
		 */
		this.get = (tableName, key) => {
			return new Promise((resolve, reject) => {
				const request = IDBDatabase.transaction([tableName], 'readwrite').objectStore(tableName).get(key)
				request.onsuccess = () => {
					resolve(request.result || null)
				}
				request.onerror = e => {
					reject(e)
				}
			})
		}

		/**
		 * 根据索引获取行
		 * @param {string} tableName 表名
		 * @param {string} indexName 索引库名称
		 * @param {string} indexVal 索引值
		 * @returns {promise}
		 */
		this.indexGet = (tableName, indexName, indexVal) => {
			return new Promise((resolve, reject) => {
				const request = IDBDatabase.transaction([tableName], 'readwrite').objectStore(tableName).index(indexName).get(indexVal)
				request.onsuccess = () => {
					resolve(request.result || null)
				}
				request.onerror = e => {
					reject(e)
				}
			})
		}

		/**
		 * 获取所有行
		 * @param {string} tableName 表名
		 * @returns {promise}
		 */
		this.getAll = (tableName) => {
			return new Promise((resolve, reject) => {
				const request = IDBDatabase.transaction([tableName], 'readwrite').objectStore(tableName).getAll()
				request.onsuccess = () => {
					resolve(request.result || null)
				}
				request.onerror = e => {
					reject(e)
				}
			})
		}

		/**
		 * 清空表
		 * @param {string} tableName 表名
		 * @returns {promise}
		 */
		this.clear = (tableName) => {
			return new Promise((resolve, reject) => {
				const request = IDBDatabase.transaction([tableName], 'readwrite').objectStore(tableName).clear()
				request.onsuccess = e => {
					resolve(e)
				}
				request.onerror = err => {
					reject(err)
				}
			})
		}

		/**
		 * 获取表信息
		 * @returns {IDBObjectStore}
		 */
		this.getTable = (tableName) => {
			const request = IDBDatabase.transaction([tableName], 'readwrite').objectStore(tableName)
			return request
		}

		/**
		 * 获取所有的表
		 * @returns {[IDBObjectStore]}
		 */
		this.getTables = () => {
			const tables = []
			for (let item of IDBDatabase.objectStoreNames) {
				tables.push(IDBDatabase.transaction([item], 'readwrite').objectStore(item))
			}
			return tables
		}

		/**
		 * 关闭数据库连接
		 * @returns {}
		 */
		this.close = () => {
			return IDBDatabase.close()
		}
	}
}