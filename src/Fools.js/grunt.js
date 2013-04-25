module.exports = function(grunt) {
  grunt.initConfig({
    pkg: '<json:package.json>',
    meta: {
      banner: '/*! <%= pkg.name %> - v<%= pkg.version %> - ' +
        '<%= grunt.template.today("yyyy-mm-dd") %>\n' +
        '<%= pkg.homepage ? "* " + pkg.homepage + "\n" : "" %>' +
        '* Copyright (c) <%= grunt.template.today("yyyy") %> <%= pkg.author.name %>;' +
        ' Licensed <%= _.pluck(pkg.licenses, "type").join(", ") %> */'
    },
    concat: {
      dist: {
        src: ['<banner:meta.banner>', 'build/app/*.js'],
        dest: 'dist/<%= pkg.name %>.js'
      }
    },
    min: {
      dist: {
        src: ['<banner:meta.banner>', '<config:concat.dist.dest>'],
        dest: 'dist/<%= pkg.name %>.min.js'
      }
    },
    lint: {
      files: ['grunt.js', 'build/**/*.js']
    },
	  coffeelint: {
		  app: {
			  files: [ 'lib/**/*.coffee' ],
			  options: {
				  no_tabs: { level: "ignore" }
			  }
		  },
		  tests: {
			  files: [ 'tests/**/*.coffee' ],
			  options: {
				  no_tabs: { level: "ignore" }
			  }
		  }
	  },
	  coffee: {
		  app : {
			  src: [ 'lib/**/*.coffee' ],
			  dest: 'build/app'
		  },
		  tests : {
			  src: [ 'tests/**/*.coffee' ],
			  dest: 'build/test'
		  }
	  },
    watch: {
      files: '<config:coffeelint.files>',
      tasks: 'coffeelint simplemocha'
    },
    jshint: {
      options: {
        curly: true,
        eqeqeq: true,
        immed: true,
        latedef: true,
        newcap: true,
        noarg: true,
        sub: true,
        undef: true,
        boss: true,
        eqnull: true
      },
      globals: {
        exports: true,
        module: false
      }
    },
    uglify: {},
		simplemocha: {
			all: {
				src: [ 'test/support/node.js', 'test/**/*.coffee'],
				options: {
					timeout: 500,
					ignoreLeaks: false,
					ui: 'bdd',
					reporter: 'progress',
					compilers: "coffee:coffee-script"
				}
			}
		}
	});

	grunt.loadNpmTasks('grunt-simple-mocha');
	grunt.loadNpmTasks('grunt-coffee');
	grunt.loadNpmTasks('grunt-coffeelint');

	// Default task.
  grunt.registerTask('default', 'coffeelint simplemocha coffee concat min');
};
